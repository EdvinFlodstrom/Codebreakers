using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private bool horizontalLaser;

    [SerializeField] private float damage;

    [SerializeField] private Transform bottomObject;
    [SerializeField] private Transform topObject;
    [SerializeField] private float laserScaleValueY;

    private BoxCollider2D boxCollider;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (!horizontalLaser)
        {
            transform.position = new Vector3(transform.position.x + transform.position.x, bottomObject.position.y + topObject.position.y) / 2f;
            transform.localScale = new Vector3(transform.localScale.x, topObject.transform.position.y - bottomObject.position.y - laserScaleValueY, transform.localScale.z);
        }
        else
        {
            transform.position = new Vector3(topObject.transform.position.x + bottomObject.transform.position.x, transform.position.y + transform.position.y) / 2f;
            transform.localScale = new Vector3(transform.localScale.x, topObject.transform.position.y - bottomObject.position.y - laserScaleValueY, transform.localScale.z);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}