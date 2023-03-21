using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject[] regularLasers;
    [SerializeField] private GameObject[] rotatedLasers;
    [SerializeField] private GameObject[] rotatedLasersLeft;
    [SerializeField] public Transform firePointRegular;
    [SerializeField] public Transform firePointRotated;

    private float attackWait = Mathf.Infinity;

    private bool aimUp;
    private bool left;

    void Awake()
    {
        aimUp = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            aimUp = true;
        }
        else
        {
            aimUp = false;
        }

        if (Mathf.Sign(transform.localScale.x) > 0)
        {
            left = false;
        }
        else left = true;

        attackWait += Time.deltaTime;

        if ((Input.GetMouseButton(0) || (Input.GetKey(KeyCode.E))) && (attackWait > attackCooldown))
        {
            Attack(aimUp);
        }
    }

    private void Attack(bool _up)
    {
        attackWait = 0;
        
        if (_up && !left)
        {
            rotatedLasers[Projectile("rotated")].transform.position = firePointRotated.position;
            rotatedLasers[Projectile("rotated")].GetComponent<PlayerProjectile>().Direction(Mathf.Sign(transform.localScale.x));
        }
        else if (!_up)
        {
            regularLasers[Projectile("regular")].transform.position = firePointRegular.position;
            regularLasers[Projectile("regular")].GetComponent<PlayerProjectile>().Direction(Mathf.Sign(transform.localScale.x));
        }
        else
        {
            rotatedLasersLeft[Projectile("rotatedLeft")].transform.position = firePointRotated.position;
            rotatedLasersLeft[Projectile("rotatedLeft")].GetComponent<PlayerProjectile>().Direction(Mathf.Sign(transform.localScale.x));
        }
    }

    private int Projectile(string _type)
    {
        if (_type == "regular")
        {
            for (int i = 0; i < regularLasers.Length; i++)
            {
                if (!regularLasers[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }
        else if (_type == "rotated")
        {
            for (int i = 0; i < rotatedLasers.Length; i++)
            {
                if (!rotatedLasers[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }
        else
        {
            for (int i = 0; i < rotatedLasersLeft.Length; i++)
            {
                if (!rotatedLasersLeft[i].activeInHierarchy)
                    return i;
            }
            return 0;
        }
    }
}
