using System.Collections;
using UnityEngine;

public class Scene6Room : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private GameObject playerObject;
    [SerializeField] private GameObject cameraObject;
    
    [Header("Bossfight related objects")]
    [SerializeField] private GameObject icewolfObject;
    [SerializeField] private GameObject icewolfBallObject;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject leftCornerTop;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boxCollider.enabled = false;
            playerObject = collision.gameObject;
            
            playerObject.GetComponent<PlayerMovement>().enabled = false;
            playerObject.GetComponent<PlayerAttack>().enabled = false;
            playerObject.GetComponent<PlayerMovement>().body.velocity = new Vector2(0, playerObject.GetComponent<PlayerMovement>().body.velocity.y);
            playerObject.GetComponent<PlayerMovement>().anim.SetBool("run", false);

            StartCoroutine(ActivateBossFight());
        }
    }
    IEnumerator ActivateBossFight()
    {
        cameraObject.GetComponent<CameraMovement>().follow = "Icewolf";
        yield return new WaitForSeconds(1.5f);
        icewolfBallObject.SetActive(true);
        icewolfObject.GetComponent<IcewolfRoute>().anim.SetTrigger("bossfight");
        leftCornerTop.SetActive(false);
        leftWall.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cameraObject.GetComponent<CameraMovement>().follow = "Bossfight";
        yield return new WaitForSeconds(1.5f);
        icewolfObject.GetComponent<IcewolfFightStart>().InitiateFight();
        playerObject.GetComponent<PlayerMovement>().enabled = true;
        playerObject.GetComponent<PlayerAttack>().enabled = true;
        icewolfBallObject.GetComponent<IcewolfBallRoute>().body.gravityScale = 1;
    }
}
