using UnityEngine;

public class ShootingPenguinProjectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private float maxFlightDuration;

    private float flightDuration;
    private float direction;
    private bool projectileHit;

    private BoxCollider2D boxCollider;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (projectileHit) return;

        flightDuration += Time.deltaTime;


        float speed = projectileSpeed * Time.deltaTime * direction;
        transform.Translate(speed, 0, 0);

        if (flightDuration > maxFlightDuration) gameObject.SetActive(false);

    }
    public void ActivateProjectile(float _direction)
    {
        flightDuration = 0;
        direction = _direction;
        gameObject.SetActive(true);
        projectileHit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (col.tag != "Enemy")
        {
            projectileHit = true;
            boxCollider.enabled = false;
            gameObject.SetActive(false);
        }    
    }
}
