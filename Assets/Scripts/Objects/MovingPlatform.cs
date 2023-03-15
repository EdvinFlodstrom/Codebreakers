using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;
    [SerializeField] private Transform platform;
    [SerializeField] private BoxCollider2D boxCollider;
    public float movement;

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
        movement = _direction * speed;
        platform.position = new Vector3(platform.position.x + Time.deltaTime * movement, platform.position.y, platform.position.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().platformName == gameObject.name)
        {
            collision.collider.transform.parent.SetParent(transform);
            //collision.gameObject.GetComponent<PlayerMovement>().onPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().platformName == gameObject.name)
        {
            collision.collider.transform.parent.SetParent(null);
            //collision.gameObject.GetComponent<PlayerMovement>().onPlatform = false;
        }
    }
}
