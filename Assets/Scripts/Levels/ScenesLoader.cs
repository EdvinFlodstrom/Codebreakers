using UnityEngine;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private int sceneNumber;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneHandler.SceneNumber(sceneNumber);
        }
    }
}
