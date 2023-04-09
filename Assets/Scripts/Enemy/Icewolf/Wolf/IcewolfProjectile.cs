using UnityEngine;

public class IcewolfProjectile : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float damage;
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

    [SerializeField] private BoxCollider2D boxCollider;
    private Animator anim;

    private bool projectileHit;
    private float direction;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (projectileHit) return;

        transform.position = new Vector3(transform.position.x + Time.deltaTime * -speedX * direction, transform.position.y + Time.deltaTime * -speedY, transform.position.z);
    }
    public void ProjectileDirection(float _direction)
    {
        direction = _direction;

        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        if (direction == -1) transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -42);
        else transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 42);

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
        if (collision.tag != "PlayerProjectile")
        {
            projectileHit = true;
            anim.SetTrigger("explode");
            SoundManager.sound.PlaySound(explosionSound);
            boxCollider.enabled = false;
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
