using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Checkpoint checkpoint;
    [SerializeField] private Transform startingPosition;
    [Header("First checkpoint is starting position")]
    [SerializeField] private GameObject[] checkpointsList;

    [SerializeField] private Healthbar healthBar;

    public int startingHealth;
    public int maxHealth;

    [Header("Disable on death")]
    [SerializeField] private Behaviour[] components;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip heartPickupSound;

    [Header("Special hearts list")]
    [SerializeField] private GameObject[] specialHearts;
    public float currentHealth { get; private set; }
    public bool invulnerable;
    private float invulnerabilityDuration = 1;
    public bool gameOver;

    private SpriteRenderer spi;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private float gravityScale;

    void Awake()
    {
        gameOver = false;
        spi = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        currentHealth = startingHealth;
        gravityScale = rigidBody.gravityScale;

        transform.position = checkpointsList[PlayerPrefs.GetInt("CheckpointNumber")].transform.position;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (PlayerPrefs.GetInt("PlayerMaxHealth") < maxHealth)
            {
                specialHearts[0].SetActive(true);
            }
            if (PlayerPrefs.GetInt("PlayerMaxHealth") > maxHealth)
            {
                AddHeart("Special", 1);
            }
        }
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
            DeathOrRespawn("Death");
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
        if (col.gameObject.CompareTag("Heart"))
        {
            AddHeart("Regular", 1);
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.CompareTag("SpecialHeart"))
        {
            SoundManager.sound.PlaySound(heartPickupSound);
            AddHeart("Special", 1);            
            col.gameObject.SetActive(false);
        }
    }
    public void AddHeart(string _type, float _hearts)
    {
        if (_type == "Regular")
        {
            SoundManager.sound.PlaySound(heartPickupSound);
            if (currentHealth < maxHealth)
            {
                currentHealth = Mathf.Clamp(currentHealth + _hearts, 0, startingHealth);
            }
        }
        else if (_type == "Special")
        {
            healthBar.FourthHeart();
            maxHealth = (int)(maxHealth + _hearts);
            currentHealth = maxHealth;
            PlayerPrefs.SetInt("PlayerMaxHealth", maxHealth);
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void DeathOrRespawn(string _type)
    {
        if (_type == "Death")
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
        else
        {
            anim.SetTrigger("respawn");
            foreach (Behaviour component in components)
                component.enabled = true;
            boxCollider.enabled = true;
            rigidBody.gravityScale = gravityScale;
            gameOver = false;
        }
    }
}