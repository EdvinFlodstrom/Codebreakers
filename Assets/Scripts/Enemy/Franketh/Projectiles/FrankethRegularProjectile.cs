using UnityEngine;

public class FrankethRegularProjectile : MonoBehaviour
{
    [SerializeField] private bool scene5Projectile;

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private AudioClip explosionSound;
    private Animator anim;

    private bool projectileHit;
    private int projectileMaxFlightTime = 10;
    private float projectileCurrentFlightTime;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        projectileCurrentFlightTime += Time.deltaTime;

        if (projectileCurrentFlightTime > projectileMaxFlightTime)
        {
            ExplodeProjectile();
            projectileCurrentFlightTime = 0;
        }
        if (projectileHit) return;

        transform.position = new Vector3(transform.position.x + Time.deltaTime * -speed, transform.position.y, transform.position.z);
    }
    public void ActivateProjectile()
    {
        projectileCurrentFlightTime = 0;
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
        if (collision.tag != "Enemy" && collision.tag != "Boss" && collision.tag != "BossProjectile" && collision.tag != "PlayerProjectile" && collision.tag != "Heart" && collision.tag != "SpecialHeart" && collision.tag != "Unshootable")
        {
            ExplodeProjectile();
        }  
    }
    private void ExplodeProjectile()
    {
        projectileHit = true;
        anim.SetTrigger("explode");
        boxCollider.enabled = false;
        if (!scene5Projectile) SoundManager.sound.PlaySound(explosionSound);
        else
        {
            if (GetComponent<EnemySoundRange>().PlayerInSoundRange()) SoundManager.sound.PlaySound(explosionSound);
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}