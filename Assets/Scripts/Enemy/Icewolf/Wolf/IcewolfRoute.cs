using UnityEngine;

public class IcewolfRoute : MonoBehaviour
{
    [Header("Leave 'Anim' empty")]
    public Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float damage;

    

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
