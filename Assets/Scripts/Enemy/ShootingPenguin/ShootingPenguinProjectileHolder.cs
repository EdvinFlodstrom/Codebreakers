using UnityEngine;

public class ShootingPenguinProjectileHolder : MonoBehaviour
{
    [SerializeField] private Transform penguin;

    void Start()
    {
        
    }

    void Update()
    {
        transform.localScale = penguin.localScale;
    }
}
