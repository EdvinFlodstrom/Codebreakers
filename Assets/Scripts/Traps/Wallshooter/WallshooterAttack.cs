using UnityEngine;

public class WallshooterAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    private Animator anim;

    private float attackWait;
    private double attackCooldown = 0.75;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if (PlayerInRange())
        {
            anim.SetBool("playerSighted", true);
            if (attackWait > attackCooldown)
            {
                attackWait = 0;
                Attack();
            }
        }
        else anim.SetBool("playerSighted", false);
    }
    private void Attack()
    {
        projectiles[Projectile()].transform.position = firePoint.position;
        projectiles[Projectile()].GetComponent<WallshooterProjectile>().ActivateProjectile();
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
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x - (float)4.5, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y - (float)0.35), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x - (float)4.5, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y - (float)0.35));
    }
}
