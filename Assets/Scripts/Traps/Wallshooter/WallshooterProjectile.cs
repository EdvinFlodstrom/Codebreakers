using UnityEngine;

public class WallshooterProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float flightDurationMax;
    [SerializeField] private int damage;
    private int direction;
    private bool projectileHit;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private float flightDuration;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (projectileHit) return;
        
        flightDuration += Time.deltaTime;

        float speed = projectileSpeed * Time.deltaTime * direction;
        transform.Translate(speed, 0, 0);

        if (flightDuration > flightDurationMax) gameObject.SetActive(false);

    }
    public void ActivateProjectile(int _direction)
    {
        direction = _direction;
        projectileHit = false;
        flightDuration = 0;
        gameObject.SetActive(true);
        if (direction == 1) transform.localScale = new Vector2(-1, transform.localScale.y);
        boxCollider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        anim.SetTrigger("explode");
        boxCollider.enabled = false;
        projectileHit = true;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
