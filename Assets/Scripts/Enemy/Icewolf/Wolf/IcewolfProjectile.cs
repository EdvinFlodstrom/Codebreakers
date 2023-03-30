using UnityEngine;

public class IcewolfProjectile : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    void Awake()
    {
        transform.position = firepoint.position;
    }

    void Update()
    {
        
    }
    public void ProjectileDirection(float _direction)
    {

    }
}
