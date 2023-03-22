using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private Transform bottomObject;
    [SerializeField] private Transform topObject;

    private BoxCollider2D boxCollider;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(transform.position.x + transform.position.x, bottomObject.position.y + topObject.position.y) / 2f;
        //^Placerar lasern i mitten (y-led) av de två objekten
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
