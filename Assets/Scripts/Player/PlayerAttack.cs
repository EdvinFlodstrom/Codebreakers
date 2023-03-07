using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject[] lasers;
    [SerializeField] public Transform firePoint;


    private float attackWait = Mathf.Infinity;

    void Start()
    {
        
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if ((Input.GetMouseButton(0) || (Input.GetKey(KeyCode.LeftShift))) && (attackWait > attackCooldown))
        {
            Attack();
        }
    }

    private void Attack()
    {
        attackWait = 0;
        

        lasers[Projectile()].transform.position = firePoint.position;
        lasers[Projectile()].GetComponent<PlayerProjectile>().Direction(Mathf.Sign(transform.localScale.x));
    }

    private int Projectile()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if (!lasers[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
