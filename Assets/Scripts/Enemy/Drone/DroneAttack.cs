using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject[] bombs;
    [SerializeField] private float attackCooldown;
    private float attackWait;

    void Update()
    {
        attackWait += Time.deltaTime;

        if (attackWait > attackCooldown)
        {
            attackWait = 0;
            Attack();
        }
    }
    private void Attack()
    {
        bombs[Bombs()].transform.position = firePoint.position;
        bombs[Bombs()].GetComponent<DroneProjectile>().ActivateProjectile();
    }
    private int Bombs()
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            if (!bombs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
