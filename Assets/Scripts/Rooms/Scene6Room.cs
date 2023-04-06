using System.Collections;
using UnityEngine;

public class Scene6Room : MonoBehaviour
{
    [SerializeField] private BossHealthbar bossHealthbar;
    [SerializeField] private GameObject icewolfHolder;
    [SerializeField] private GameObject frankethHolder;
    private BoxCollider2D boxCollider;
    private GameObject playerObject;
    [SerializeField] private GameObject cameraObject;
    private bool moveRightWall;
    
    [Header("Bossfight related objects")]
    [SerializeField] private GameObject icewolfObject;
    [SerializeField] private GameObject icewolfBallObject;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject leftCornerTop;

    [Header("Franketh time...")]
    [SerializeField] private Transform rightWall;
    [SerializeField] private float rightWallSpeed;
    [SerializeField] private Transform frankethPosition;

    [Header("Franketh")]
    [SerializeField] private float frankethSlideinSpeed;
    [SerializeField] private GameObject frankethObject;
    [SerializeField] private SpriteRenderer frankethRend;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (moveRightWall) MoveRightWall();
        if (!frankethHolder.activeInHierarchy) return;

        if (frankethHolder.transform.position.x > frankethPosition.position.x)
            frankethHolder.transform.position = new Vector3(frankethHolder.transform.position.x + Time.deltaTime * frankethSlideinSpeed * -1, frankethHolder.transform.position.y, frankethHolder.transform.position.z);
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
        yield return new WaitForSeconds(0.01f);
        bossHealthbar.ActivateIcewolf();
        playerObject.GetComponent<PlayerMovement>().enabled = true;
        playerObject.GetComponent<PlayerAttack>().enabled = true;
        icewolfBallObject.GetComponent<IcewolfBallRoute>().body.gravityScale = 1;
    }
    public IEnumerator FrankethTime()
    {
        icewolfHolder.SetActive(false);
        moveRightWall = true;
        yield return new WaitForSeconds(1.5f);
        moveRightWall = false;
        rightWall.gameObject.SetActive(false);
        frankethHolder.SetActive(true);

        yield return new WaitForSeconds(3.5f);
        frankethObject.GetComponent<EnemyHealth>().enabled = true;
        yield return new WaitForSeconds(0.01f);
        bossHealthbar.ActivateFranketh();
        frankethObject.GetComponent<FrankethAttack>().enabled = true;
        frankethObject.tag = "Enemy";
    }
    public IEnumerator FrankethSecondPhase()
    {
        frankethObject.tag = "Invulnerable";
        frankethObject.GetComponent<FrankethAttack>().enabled = false;
        yield return new WaitForSeconds(1);

        Color c = frankethRend.material.color;
        for (float colour = 1; colour > 0; colour -= 0.01f)
        {
            c.b = colour;
            frankethRend.color = new Color(colour, colour, 1);
            yield return new WaitForSeconds(0.01f);
        }
        //Phase 2 has begun...
    }
    private void MoveRightWall()
    {
        rightWall.position = new Vector3(rightWall.position.x + Time.deltaTime * 1 * rightWallSpeed, rightWall.position.y, rightWall.position.z);
    }
}
