using UnityEngine;

public class ShootingPenguinAttack : MonoBehaviour
{
    [SerializeField] int attackPower;
    [SerializeField] float rayDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerLocation;
    private BoxCollider2D boxCollider;
    private bool playerSpotted;
    private Animator anim;
    SpriteRenderer spi;

    void Start()
    {
        anim = GetComponent<Animator>();
        spi = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (PlayerInRange())
        {
            anim.SetBool("playerInRange", true);
            if (playerLocation.position.x > transform.position.x)
            {
                spi.flipX = true;
            }
            else if (playerLocation.position.x < transform.position.x)
            {
                spi.flipX = false;
            }
            Attack();
        }
        else anim.SetBool("playerInRange", false);
        
    }
    private bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void Attack()
    {

    }
}
