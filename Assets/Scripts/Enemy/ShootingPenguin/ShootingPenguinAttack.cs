using UnityEngine;
using System.Threading;

public class ShootingPenguinAttack : MonoBehaviour
{
    [SerializeField] int attackPower;
    [SerializeField] float rayDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private Transform holderPosition;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;

    [SerializeField] private BoxCollider2D boxCollider;
    private float attackWait;
    private double attackCooldown = 1;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attackWait += Time.deltaTime;
        if (attackWait > 0.64) anim.SetBool("attack", false);

        if (PlayerInRange())
        {
            if (playerLocation.position.x > holderPosition.position.x)
            {
                gameObject.transform.localScale = new Vector3(-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else if (playerLocation.position.x < holderPosition.position.x)
            {
                gameObject.transform.localScale = new Vector3(1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            if (attackWait > attackCooldown)
            {
                anim.SetBool("attack", true);
                attackWait = 0;
                Attack();
            }
        }
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
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(boxCollider.bounds.size.x * rayDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    //}
}