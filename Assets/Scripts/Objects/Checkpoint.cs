using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;

    private Animator anim;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            anim.SetTrigger("enabling");
            PlayerPrefs.SetInt("CheckpointNumber", checkpointNumber);
        }
    }
    private void CheckpointActivated()
    {
        anim.SetTrigger("enabled");
    }
}
