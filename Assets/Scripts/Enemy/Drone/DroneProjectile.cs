using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private AudioClip explosionSound;

    private Rigidbody2D body;
    private Animator anim;

    private GameObject player;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider.enabled = false;
    }
    public void ActivateProjectile()
    {
        gameObject.SetActive(true);
        boxCollider.enabled = true;
        body.constraints = RigidbodyConstraints2D.None;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerHit())
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        if (collision.gameObject.tag != "Unshootable")
        {
            SoundManager.sound.PlaySound(explosionSound);
            anim.SetTrigger("explode");
            boxCollider.enabled = false;
            body.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        }
    }
    private bool PlayerHit()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y) * explosionRadius, 0, Vector2.zero, 0, playerLayer);
        if (hit.collider != null) player = hit.collider.gameObject;
        return hit.collider != null;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
//   private void OnDrawGizmos()
//   {
//       Gizmos.color = Color.red;
//       Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y) * explosionRadius);
//   }
}