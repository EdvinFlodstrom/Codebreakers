using System.Collections;
using UnityEngine;

public class Scene5DisableProjectiles : MonoBehaviour
{
    [SerializeField] GameObject scene5Projectiles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scene5Projectiles.GetComponent<Scene5Projectiles>().shoot = false;
        }
    }
}
