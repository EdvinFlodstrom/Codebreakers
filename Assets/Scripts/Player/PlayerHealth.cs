using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private Healthbar healthBar;

    [SerializeField] public int startingHealth;
    [SerializeField] public int maxHealth;

    [Header("Components to be disabled")]
    [SerializeField] private Behaviour[] components;

    public float currentHealth { get; private set; }
    private bool invulnerable;
    private int invulnerabilityDuration = 1;
    public bool gameOver;
    private bool touchingEnemy;

    private SpriteRenderer spi;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    void Start()
    {
        gameOver = false;
        spi = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (touchingEnemy && invulnerable == false)
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerability());
        }
        else if (currentHealth == 0 || currentHealth < 0)
        {
            foreach (Behaviour component in components)
                component.enabled = false;
            boxCollider.enabled = false;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            anim.SetTrigger("dead");
            gameOver = true;
        }
        

    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;

        spi.color = new Color(1, 0, 0, 0.85f);
        yield return new WaitForSeconds(invulnerabilityDuration);
        spi.color = Color.white;

        invulnerable = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            touchingEnemy = true;
        }

        if (col.gameObject.tag == "Heart")
        {
            addHeart(1);
            col.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
        }
    }
    private void addHeart(float _hearts)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + _hearts, 0, startingHealth);
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
