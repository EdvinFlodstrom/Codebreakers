using UnityEngine;

public class IcewolfBallRoute : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private bool movingRight;

    private Animator anim;
    public Rigidbody2D body;
    private bool hasLanded;

    void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hasLanded) return;

        anim.SetBool("rolling", true);
        if (movingRight)
        {
            MovementDirection(1);
            if (transform.position.x > rightEdge.position.x)
                movingRight = !movingRight;
        }
        else
        {
            MovementDirection(-1);
            if (transform.position.x < leftEdge.position.x)
                movingRight = !movingRight;
        }
    }
    private void MovementDirection(float _direction)
    {
        if (_direction == 1) transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed * _direction, transform.position.y, transform.position.z);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") hasLanded = true;
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
    private void DisableGravity()
    {
        body.gravityScale = 0;
    }
}