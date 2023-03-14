using UnityEngine;

public class TileTrap : MonoBehaviour
{
    [SerializeField] private SpriteRenderer RegularTile;
    [SerializeField] private SpriteRenderer RedTile;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RegularTile.enabled = false;
            RedTile.enabled = true;

            boxCollider.enabled = false;
        }
    }
}
