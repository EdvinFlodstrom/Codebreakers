using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private int targetNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            tutorial.Targets(targetNumber);
        }        
    }
}
