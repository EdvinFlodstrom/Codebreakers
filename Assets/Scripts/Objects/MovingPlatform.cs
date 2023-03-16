using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;
    [SerializeField] private Transform platform;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private Transform player;
    public float movement;

    private bool movingLeft;
    private bool lockPlayer;
    private GameObject playerObject;
    void Start()
    {
        
    }

    void Update()
    {
        PlayerOnPlatform();

        if (lockPlayer) playerObject.transform.parent.SetParent(transform);

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
    private bool PlayerOnPlatform()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y + (float)0.2, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x - (float)0.075, boxCollider.bounds.size.y), 1, Vector2.down, 0.1f, playerLayer);
        if (hit.collider != null)
        {
            player = hit.collider.gameObject.transform.parent;
            player.transform.SetParent(transform);
        }
        else
        {
            if (player != null) player.transform.SetParent(null);
        }
        return hit.collider != null;
    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube((new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y + (float)0.2, boxCollider.bounds.center.z)), new Vector2(boxCollider.bounds.size.x - (float)0.075, boxCollider.bounds.size.y));
//    }
}
