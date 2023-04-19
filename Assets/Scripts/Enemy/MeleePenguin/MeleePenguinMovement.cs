using UnityEngine;

public class MeleePenguinMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;

    private bool movingLeft;
    [SerializeField] private int damage;

    [SerializeField] private Transform penguin;

    [SerializeField] private AudioClip penguinWalkSound;
    [SerializeField] private float penguinWalkSoundCooldown;
    private float penguinWalkSoundWait;

    private BoxCollider2D boxCollider;
    private Animator anim;
    private PlayerHealth playerHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        penguinWalkSoundWait += Time.deltaTime;

        if (penguinWalkSoundWait > penguinWalkSoundCooldown && GetComponent<EnemySoundRange>().PlayerInSoundRange())
        {
            penguinWalkSoundWait = 0;
            SoundManager.sound.PlaySound(penguinWalkSound);
        }

        if (movingLeft)
        {
            if (penguin.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                movingLeft = !movingLeft;
        }
        else
        {
            if (penguin.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                movingLeft = !movingLeft;

            if(movingLeft == false)
            {
                penguin.transform.localScale = new Vector3(-1, penguin.transform.localScale.y, penguin.transform.localScale.z);
            }
            else penguin.transform.localScale = new Vector3(1, penguin.transform.localScale.y, penguin.transform.localScale.z);
        }
    }

    private void MoveInDirection(int _direction)
    {
        penguin.position = new Vector3(penguin.position.x + Time.deltaTime * _direction * speed, penguin.position.y, penguin.position.z);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
