using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float upDistance;
    private float lookAhead;


    [SerializeField] private Transform icewolfCamera;
    private float towardsIcewolfX;
    private float towardsIcewolfY;

    [Header("Camera to Icewolf X axis")]
    [SerializeField] private float toIcewolfValue;
    [SerializeField] private float toIcewolfSpeed;

    [Header("Camera to Icewolf Y axis")]
    [SerializeField] private float toIcewolfValueY;
    [SerializeField] private float toIcewolfSpeedY;


    public string follow = "Player";

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (follow == "Player") FollowPlayer();
        if (follow == "Icewolf") Icewolf();
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
    }
    //--Code broken--effekt?
    //towardsIcewolf = Mathf.Lerp(aheadDistance, (toIcewolfValue* player.localScale.x), Time.deltaTime* toIcewolfSpeed);
    //transform.position = new Vector3(player.position.x + towardsIcewolf, transform.position.y, transform.position.z);
    //Towards icewolf: 1
    //To icewolf value: 10
    //To icewolf speed: 5
}
