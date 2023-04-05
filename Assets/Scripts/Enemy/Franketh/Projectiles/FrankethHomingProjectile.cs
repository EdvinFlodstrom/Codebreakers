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
    }
    public void ActivateProjectile()
    {
        projectileHealth = projectileHealthMax;
        projectileHit = false;
        boxCollider.enabled = true;
        gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            projectileHealth = projectileHealth - collision.gameObject.GetComponent<PlayerProjectile>().damage;
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Boss" && collision.gameObject.tag != "PlayerProjectile" && collision.gameObject.tag != "BossProjectile")
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