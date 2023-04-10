using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Scene6Room bossRoom;
    [SerializeField] private float speed;
    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float upDistance;
    private float lookAhead;


    [SerializeField] private Transform icewolfCamera;
    private float towardsIcewolfX;
    private float towardsIcewolfY;
    private float zoomOnIcewolf;

    private float towardsCenterX;
    private float towardsCenterY;
    private float zoomTowardsCenter;

    [Header("Camera to Icewolf X axis")]
    [SerializeField] private float toIcewolfValue;
    [SerializeField] private float toIcewolfSpeed;

    [Header("Camera to Icewolf Y axis")]
    [SerializeField] private float toIcewolfValueY;
    [SerializeField] private float toIcewolfSpeedY;

    [Header("Camera zoom on Icewolf")]
    [SerializeField] private float icewolfZoomValue;
    [SerializeField] private float icewolfZoomSpeed;

    [Header("Camera To center")]
    [SerializeField] private float toCenterValueX;
    [SerializeField] private float toCenterSpeedX;

    [SerializeField] private float toCenterValueY;
    [SerializeField] private float toCenterSpeedY;

    [SerializeField] private float centerZoomValue;
    [SerializeField] private float centerZoomSpeed;

    public string follow = "Player";

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (follow == "Player") FollowPlayer();
        if (follow == "Icewolf") Icewolf();
        if (follow == "Bossfight") Bossfight();
    }
    private void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y + upDistance, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
    private void Icewolf()
    {
        towardsIcewolfX = Mathf.Lerp(towardsIcewolfX, (toIcewolfValue * player.localScale.x), Time.deltaTime * toIcewolfSpeed);
        towardsIcewolfY = Mathf.Lerp(towardsIcewolfY, (toIcewolfValueY * player.localScale.x), Time.deltaTime * toIcewolfSpeedY);
        transform.position = new Vector3(player.position.x + lookAhead + towardsIcewolfX, player.position.y + upDistance + towardsIcewolfY, transform.position.z);
        zoomOnIcewolf = Mathf.Lerp(zoomOnIcewolf, icewolfZoomValue, Time.deltaTime * icewolfZoomSpeed);
        GetComponent<Camera>().orthographicSize = 4.5f - zoomOnIcewolf;

        currentPosX = transform.position.x;
        currentPosY = transform.position.y;
    }
    private void Bossfight()
    {
        zoomTowardsCenter = Mathf.Lerp(zoomTowardsCenter, centerZoomValue, Time.deltaTime * centerZoomSpeed);
        GetComponent<Camera>().orthographicSize = (4.5f - zoomOnIcewolf) + zoomTowardsCenter;

        towardsCenterX = Mathf.Lerp(towardsCenterX, (toCenterValueX), Time.deltaTime * toCenterSpeedX);
        towardsCenterY = Mathf.Lerp(towardsCenterY, (toCenterValueY), Time.deltaTime * toCenterSpeedY);
        transform.position = new Vector3(currentPosX - towardsCenterX, currentPosY - towardsCenterY, transform.position.z);
    }
    public IEnumerator CodeBroken()
    {
        for (float time = 0; time < 0.5; time += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x + .1f, transform.position.y - .1f, transform.position.z);
            yield return new WaitForSeconds(0.05f);
            transform.position = new Vector3(transform.position.x - 1f, transform.position.y + .1f, transform.position.z);
            yield return new WaitForSeconds(0.05f);
        }
        bossRoom.StartCoroutine(bossRoom.Death());
    }
}
