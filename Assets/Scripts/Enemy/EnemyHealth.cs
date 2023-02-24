using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    private float enemyCurrentHealth;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        enemyCurrentHealth = enemyHealth;
    }

    void Update()
    {
        
    }
    public void TakeDamage(float _damage)
    {
        enemyCurrentHealth = Mathf.Clamp(enemyCurrentHealth - _damage, 0, enemyHealth);

        if (enemyCurrentHealth > 1)
        {
            anim.SetBool("dead", false);
            gameObject.SetActive(false);
        }
    }
}
