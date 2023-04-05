using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [System.NonSerialized] public float enemyCurrentHealth;

    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Disable components on death")]
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

    public void TakeDamage(float _damage)
    {
        enemyCurrentHealth = Mathf.Clamp(enemyCurrentHealth - _damage, 0, enemyHealth);

        if (enemyCurrentHealth < 1)
        {
            SoundManager.sound.PlaySound(deathSound);
            anim.SetTrigger("dead");
            boxCollider.enabled = false;
            foreach (Behaviour component in components)
                component.enabled = false;
        }
        else SoundManager.sound.PlaySound(hurtSound);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
