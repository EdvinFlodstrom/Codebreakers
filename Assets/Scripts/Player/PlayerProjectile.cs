using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float flightDurationMax;
    [SerializeField] private int damage;

    [SerializeField] private Transform playerLocation;

    private float flightDuration;
    private float direction;

    private bool projectileHit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (projectileHit) return;

        flightDuration += Time.deltaTime;

        float speed = projectileSpeed * Time.deltaTime * direction;
        transform.Translate(speed, 0, 0);
        

        if (flightDuration > flightDurationMax) gameObject.SetActive(false);
    }

    public void Direction(float _direction)
    {
        flightDuration = 0;
        direction = _direction;
        gameObject.SetActive(true);
        projectileHit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Enemy")
        {
            projectileHit = true;
            boxCollider.enabled = false;
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
