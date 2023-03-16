using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private BoxCollider2D boxCollider;

    private Rigidbody2D body;
    private Animator anim;
    private float horizontalInput;
    private float verticalInput;
    private bool canJump;
    public bool onPlatform;


    void Start()
    {

    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        OnPlatform();
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("lookUp", true);
        }
        else
        {
            anim.SetBool("lookUp", false);
        }
            

        if (Input.GetMouseButton(0) || (Input.GetKey(KeyCode.E)))
        {
            anim.SetBool("shoot", true);
        }
        else anim.SetBool("shoot", false);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (IsGrounded())
        {
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
        }

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.LeftArrow)))
        {
            gameObject.transform.localScale = new Vector3(1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.RightArrow)))
        {
            gameObject.transform.localScale = new Vector3(-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else body.velocity = new Vector2(0, body.velocity.y);

        anim.SetBool("run", horizontalInput != 0);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetBool("jump", true);
            }
        }
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y - (float)0.7, boxCollider.bounds.center.z), new Vector2((float)0.65, (float)0.2), 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y - (float)0.7, boxCollider.bounds.center.z), new Vector2((float)0.65, (float)0.2));
//    }
    private bool OnPlatform()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y - (float)0.75, boxCollider.bounds.center.z), new Vector2((float)0.7, (float)0.1), 1, Vector2.down, 0.1f, platformLayer);
        if (hit.collider != null)
        {
            onPlatform = true;
        }
        else
        {
            //gameObject.transform.parent.transform.parent = null;
            onPlatform = false;
        }
        return hit.collider != null;
    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube((new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y - (float)0.75, boxCollider.bounds.center.z)), new Vector2((float)0.7, (float)0.1));
//    }
}
