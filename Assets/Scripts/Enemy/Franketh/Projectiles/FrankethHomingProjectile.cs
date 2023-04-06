using UnityEngine;

public class FrankethHomingProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float projectileHealthMax;
    private float projectileHealth;

    [SerializeField] private BoxCollider2D boxCollider;
    private Animator anim;

    private bool projectileHit;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (projectileHit) return;

        transform.position = new Vector3(transform.position.x + Time.deltaTime * -speed, transform.position.y, transform.position.z);

        if (projectileHealth < 1)
        {
            projectileHit = true;
            anim.SetTrigger("explode");
            boxCollider.enabled = false;
        }
    }
    public void ActivateProjectile()
    {
        projectileHealth = projectileHealthMax;
        projectileHit = false;
        boxCollider.enabled = true;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            projectileHealth = projectileHealth - collision.GetComponent<PlayerProjectile>().damage;
        }
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (collision.tag != "Enemy" && collision.tag != "Boss" && collision.tag != "PlayerProjectile" && collision.tag != "BossProjectile")
        {
            projectileHit = true;
            anim.SetTrigger("explode");
            boxCollider.enabled = false;
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}