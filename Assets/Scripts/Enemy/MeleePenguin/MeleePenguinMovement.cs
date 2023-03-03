using UnityEngine;

public class MeleePenguinMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;

    [SerializeField] private LayerMask playerLayer;
    
    private bool movingLeft;
    [SerializeField] private int damage;

    [SerializeField] private Transform penguin;

    private Animator anim;
    private BoxCollider2D boxCollider;
    private PlayerHealth playerHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
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
                penguin.transform.localScale = new Vector3((float)-0.35, penguin.transform.localScale.y, penguin.transform.localScale.z);
            }
            else penguin.transform.localScale = new Vector3((float)0.35, penguin.transform.localScale.y, penguin.transform.localScale.z);
        }
    }

    private void MoveInDirection(int _direction)
    {
        penguin.position = new Vector3(penguin.position.x + Time.deltaTime * _direction * speed, penguin.position.y, penguin.position.z);
    }
}
