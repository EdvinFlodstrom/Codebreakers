using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;
    [SerializeField] private Transform platform;

    private bool movingLeft;
    void Start()
    {
        
    }

    void Update()
    {
        if (movingLeft)
        {
            if (platform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                movingLeft = !movingLeft;
        }
        else
        {
            if (platform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                movingLeft = !movingLeft;
        }
    }
    private void MoveInDirection(int _direction)
    {
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _direction * speed, platform.position.y, platform.position.z);
    }
}
