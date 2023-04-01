using UnityEngine;

public class EnemySoundRange : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float soundRangeX;
    [SerializeField] private float soundRangeY;
    public bool PlayerInSoundRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(20, 15), 0, Vector2.down, 0.1f, playerLayer);
        return hit.collider != null;
    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(new Vector3(boxCollider.bounds.center.x, boxCollider.bounds.center.y, boxCollider.bounds.center.z), new Vector2(soundRangeX, soundRangeY));
//    }
}
