using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float verticalInput;
    private bool canJump;


    void Start()
    {

    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
            float horizontalInput = Input.GetAxis("Horizontal");

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
            if (isGrounded())
            {
                anim.SetBool("jump", false);
            }
            else
            {
                anim.SetBool("jump", true);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);
                gameObject.transform.localScale = new Vector3((float)0.25, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);
                gameObject.transform.localScale = new Vector3((float)-0.25, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            anim.SetBool("run", horizontalInput != 0);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetBool("jump", true);
            }
        }
    }
    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
}
