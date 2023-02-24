using UnityEngine;

public class ShootingPenguinProjectileHolder : MonoBehaviour
{
    [SerializeField] private Transform penguin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = penguin.localScale;
    }
}
