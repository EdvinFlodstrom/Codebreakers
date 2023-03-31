using UnityEngine;

public class IcewolfRoute : MonoBehaviour
{
    [Header("Leave 'Anim' empty")]
    public Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [SerializeField] private Transform playerPosition;

    

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
