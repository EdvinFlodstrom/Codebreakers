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

    [SerializeField] private float toEdgesValue;
    private float toLeftEdgeValue;
    private float toRightEdgeValue;

    private float toLeftEdge;
    private float toRightEdge;

    private float currentPosX;
    

    void Awake()
    {
        currentPosX = transform.position.x;
        toLeftEdgeValue = toEdgesValue;
        toRightEdgeValue = toEdgesValue;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (movingLeft)
        {
            if (transform.position.x >= leftEdge.position.x)
            {
                toLeftEdge = Mathf.Lerp(toLeftEdge, (toLeftEdgeValue * transform.localScale.x), Time.deltaTime * speed);
                transform.position = new Vector3(currentPosX - toLeftEdge, transform.position.y, transform.position.z);
            }
            else
            {
                currentPosX = leftEdge.position.x;
                movingLeft = !movingLeft;
            }
        }
        else
        {
            if (transform.position.x <= rightEdge.position.x)
            {
                toRightEdge = Mathf.Lerp(toRightEdge, (toRightEdgeValue * transform.localScale.x), Time.deltaTime * speed);
                transform.position = new Vector3(currentPosX + toRightEdge, transform.position.y, transform.position.z);
            }
            else
            {
                currentPosX = rightEdge.position.x;
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
