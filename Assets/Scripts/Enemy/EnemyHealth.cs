using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    private float enemyCurrentHealth;

    private Animator anim;
    private BoxCollider2D boxCollider;

    [Header("Components to be disabled")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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

        if (enemyCurrentHealth < 1)
        {
            anim.SetBool("dead", true);
            boxCollider.enabled = false;
            foreach (Behaviour component in components)
                component.enabled = false;
        }
    }
}
