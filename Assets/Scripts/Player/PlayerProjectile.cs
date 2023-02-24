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
        flightDuration += Time.deltaTime;

        transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime * projectileSpeed , gameObject.transform.position.y, gameObject.transform.position.z);

        if (flightDuration > flightDurationMax) gameObject.SetActive(false);
    }
    public void Direction()
    {

        //Mathf.Sign(direction - transform.position.x)
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
