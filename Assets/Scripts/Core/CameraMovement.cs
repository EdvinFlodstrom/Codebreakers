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

    public string follow = "Player";

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (follow == "Player") FollowPlayer();
        if (follow == "Kenneth") Kenneth();
    }
    private void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y + upDistance, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
    private void Kenneth()
    {

    }
}
