using UnityEngine;

public class FrankethLaser : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float speedX;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private AudioClip hitSound;
    private Animator anim;

    private bool projectileHit;
    private Transform playerPosition;

    private float laserPositionY;
    private float laserPositionX;

    private float playerPositionX;
    private Vector3 laserPosition;
    private bool keepMoving;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (projectileHit) return;

        if (keepMoving) transform.position = new Vector3(transform.position.x + Time.deltaTime * -speedX, transform.position.y, transform.position.z);

        if (playerPosition.position.y > transform.position.y)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, 0.3f);
        }
        else transform.Rotate(transform.rotation.x, transform.rotation.y, -0.3f);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPositionX, playerPosition.position.y, playerPosition.position.z), speed * Time.deltaTime);

        
        laserPosition = transform.position;
        if (laserPositionX == transform.position.x)
        {
            keepMoving = true;
        }
        laserPositionY = transform.position.y;
        laserPositionX = transform.position.x;
    }
    public void ActivateProjectile(Transform _playerPosition)
    {
        keepMoving = false;
        transform.eulerAngles = new Vector3(0, 0, 120);
        playerPosition = _playerPosition;
        playerPositionX = playerPosition.position.x;
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
        if (collision.tag != "Enemy" && collision.tag != "Boss" && collision.tag != "BossProjectile" && collision.tag != "PlayerProjectile" && collision.tag != "Heart")
        {
            projectileHit = true;
            anim.SetTrigger("explode");
            SoundManager.sound.PlaySound(hitSound);
            boxCollider.enabled = false;
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}