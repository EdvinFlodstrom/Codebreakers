using UnityEngine;

public class ShootingPenguinAttack : MonoBehaviour
{
    [SerializeField] int attackPower;
    [SerializeField] float rayDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;

    private BoxCollider2D boxCollider;
    private bool playerSpotted;
    private float attackWait;
    private double attackCooldown = 0.92;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if (PlayerInRange())
        {
            anim.SetBool("playerInRange", true);
            if (playerLocation.position.x > transform.position.x)
            {
                gameObject.transform.localScale = new Vector3((float)-0.35, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else if (playerLocation.position.x < transform.position.x)
            {
                gameObject.transform.localScale = new Vector3((float)0.35, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            if (attackWait > attackCooldown)
            {
                attackWait = 0;
                Attack();
            }

            
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
        projectiles[Projectile()].transform.position = firePoint.position;
        projectiles[Projectile()].GetComponent<ShootingPenguinProjectile>().ActivateProjectile(Mathf.Sign(transform.localScale.x));
    }
    private int Projectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
