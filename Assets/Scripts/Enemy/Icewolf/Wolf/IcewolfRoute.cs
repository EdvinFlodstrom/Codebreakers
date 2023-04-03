using UnityEngine;

public class IcewolfRoute : MonoBehaviour
{
    [Header("Leave 'Anim' empty")]
    public Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [SerializeField] private Transform playerPosition;

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private bool movingLeft;

    [Header("Camera to Icewolf X axis")]
    [SerializeField] private float movementTime;
    private float leftToRightPosX;
    private float RightToLeftPosX;

    private Vector3 velocity = Vector3.zero;



    void Awake()
    {
        leftToRightPosX = leftEdge.position.x + 0.5f;
        RightToLeftPosX = rightEdge.position.x - 0.5f;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x >= leftToRightPosX)
            {          
                transform.position = Vector3.SmoothDamp(transform.position, leftEdge.position, ref velocity, movementTime);
            }
            else
            {
                movingLeft = !movingLeft;
            }
        }
        else
        {
            if (transform.position.x <= RightToLeftPosX)
            {                
                transform.position = Vector3.SmoothDamp(transform.position, rightEdge.position, ref velocity, movementTime);
            }
            else
            {
                movingLeft = !movingLeft;
            }
        }


        if (playerPosition.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
