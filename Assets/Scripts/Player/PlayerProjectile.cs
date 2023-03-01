using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float flightDurationMax;
    [SerializeField] private int damage;
    private float flightDuration;

    private Transform direction;
    private bool projectileHit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (projectileHit) return;

        flightDuration += Time.deltaTime;

        float speed = projectileSpeed * Time.deltaTime;
        transform.Translate(speed, 0, 0);
        

        if (flightDuration > flightDurationMax) gameObject.SetActive(false);
    }
    public void Direction(float _direction)
    {
        flightDuration = 0;
        gameObject.SetActive(true);
        projectileHit = false;
        boxCollider.enabled = false;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        projectileHit = true;
        boxCollider.enabled = false;
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
