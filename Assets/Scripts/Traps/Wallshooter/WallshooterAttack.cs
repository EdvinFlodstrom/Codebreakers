using UnityEngine;

public class WallshooterAttack : MonoBehaviour
{
    [SerializeField] private bool firingRight;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private double attackCooldown;
    [SerializeField] private double initialCooldown;

    [SerializeField] private AudioClip attackSound;

    private float attackWait;
    private float initialWait;
    private int direction;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (firingRight) direction = 1;
        else direction = -1;
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if (PlayerInRange())
        {
            initialWait += Time.deltaTime;
            anim.SetBool("playerSighted", true);
            if (attackWait > attackCooldown && initialWait > initialCooldown)
            {
                attackWait = 0;
                SoundManager.sound.PlaySound(attackSound);
                Attack();
            }
        }
        else
        {
            attackWait = 0;
            initialWait = 0;
            anim.SetBool("playerSighted", false);
        }
    }
    private void Attack()
    {
        projectiles[Projectile()].transform.position = firePoint.position;
        projectiles[Projectile()].GetComponent<WallshooterProjectile>().ActivateProjectile(direction);
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
    private bool PlayerInRange()
    {
        if (firingRight)
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x + (float)4.5, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y - (float)0.35), 0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x - (float)4.5, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y - (float)0.35), 0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        if (firingRight) Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x + (float)4.5, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y - (float)0.35));
//        else Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x - (float)4.5, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y - (float)0.35));
//    }
}
