using UnityEngine;

public class FrankethLaser : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;

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
        projectileHit = false;
        boxCollider.enabled = true;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (collision.tag != "Enemy" && collision.tag != "Boss" && collision.tag != "BossProjectile" && collision.tag != "PlayerProjectile")
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