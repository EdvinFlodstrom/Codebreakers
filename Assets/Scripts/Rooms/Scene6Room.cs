using System.Collections;
using UnityEngine;

public class Scene6Room : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private GameObject playerObject;
    [SerializeField] private GameObject cameraObject;

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
        yield return new WaitForSeconds(3f);
        //Set camera for bossfight
        playerObject.GetComponent<PlayerMovement>().enabled = true;
        playerObject.GetComponent<PlayerAttack>().enabled = true;
    }
}
