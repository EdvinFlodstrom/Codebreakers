using System.Collections;
using UnityEngine;

public class TileTrap : MonoBehaviour
{
    [SerializeField] private SpriteRenderer RegularTile;
    [SerializeField] private SpriteRenderer RedTile;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject penguinTile;
    private BoxCollider2D boxCollider;
    private SpriteRenderer rend;

    [SerializeField] private AudioClip tileActivationSound;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rend = penguinTile.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RegularTile.enabled = false;
            SoundManager.sound.PlaySound(tileActivationSound);
            StartCoroutine(TileActivition());
        }
    }
    IEnumerator TileActivition()
    {
        Color c = rend.material.color;
        for (float alpha = 0f; alpha < 1; alpha += 0.1f)
        {
            c.a = alpha;
            rend.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(0.4f);
        enemy.SetActive(true);
        gameObject.SetActive(false);
    }
}
