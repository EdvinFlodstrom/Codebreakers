using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private Healthbar healthBar;

    [SerializeField] public int startingHealth;
    [SerializeField] public int maxHealth;

    [Header("Components to be disabled")]
    [SerializeField] private Behaviour[] components;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip heartPickupSound;
    public float currentHealth { get; private set; }
    public bool invulnerable;
    private float invulnerabilityDuration = 1;
    public bool gameOver;

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
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;

        SoundManager.sound.PlaySound(hurtSound);
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerability());
        }
        else if (currentHealth <= 0)
        {
            foreach (Behaviour component in components)
                component.enabled = false;
            SoundManager.sound.PlaySound(deathSound);
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
        if (col.gameObject.tag == "Heart")
        {
            SoundManager.sound.PlaySound(heartPickupSound);
            addHeart(1);
            col.gameObject.SetActive(false);
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