using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float flightDurationMax;
    [SerializeField] private int damage;

    [SerializeField] private Transform playerLocation;

    private float flightDuration;

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
        //^ ej problemet
        

        if (flightDuration > flightDurationMax) gameObject.SetActive(false);
    }
    public void ActivateProjectile()
    {
        flightDuration = 0;
        projectileHit = false;
        gameObject.SetActive(true);
        if (boxCollider.enabled == false) boxCollider.enabled = true;

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
