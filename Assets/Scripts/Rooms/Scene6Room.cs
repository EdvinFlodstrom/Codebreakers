using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class Scene6Room : MonoBehaviour
{
    [SerializeField] private BackgroundMusicManager backgroundMusic;
    [SerializeField] private GameObject scene5Projectiles;
    [SerializeField] private BossHealthbar bossHealthbar;
    [SerializeField] private FrankethAttack frankethAttack;
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

    [Header("Franketh phase 2")]
    [SerializeField] private GameObject tiletrap1;
    [SerializeField] private GameObject tiletrap2;
    [SerializeField] private GameObject pickupHeart1;
    [SerializeField] private GameObject pickupHeart2;
    [SerializeField] private GameObject penguin1;
    [SerializeField] private GameObject penguin2;

    [Header("Code Broken")]
    [SerializeField] private GameObject blackCanvas;
    [SerializeField] private SpriteRenderer blackCanvasRend;
    [SerializeField] private GameObject codeBroken;
    [SerializeField] private SpriteRenderer codeBrokenRend;
    [SerializeField] private GameObject death;
    [SerializeField] private GameObject playerHealthBar;

    delegate bool PenguinsDead();
    PenguinsDead penguinsCheck;

    private bool bothPenguinsDead;
    private bool checkPenguins;
    private bool penguin1Dead;
    private bool penguin2Dead;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (moveRightWall) MoveRightWall();
        if (checkPenguins)
        {
            penguinsCheck();
            if (penguin1Dead && penguin2Dead)
            {
                bothPenguinsDead = true;
                checkPenguins = false;
            }          
        }
        if (!frankethHolder.activeInHierarchy) return;

        if (frankethHolder.transform.position.x > frankethPosition.position.x)
            frankethHolder.transform.position = new Vector3(frankethHolder.transform.position.x + Time.deltaTime * frankethSlideinSpeed * -1, frankethHolder.transform.position.y, frankethHolder.transform.position.z);
    }
    private bool P1()
    {
        if (!penguin1.activeInHierarchy && !penguin1Dead)
        {
            penguin1Dead = true;
            pickupHeart1.transform.position = penguin1.transform.position;
            pickupHeart1.SetActive(true);
            return true;
        }        
        else return false;
    }
    private bool P2()
    {
        if (!penguin2.activeInHierarchy && !penguin2Dead)
        {
            penguin2Dead = true;
            pickupHeart2.transform.position = penguin2.transform.position;
            pickupHeart2.SetActive(true);
            return true;
        }
        else return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boxCollider.enabled = false;
            playerObject = collision.gameObject;

            Destroy(scene5Projectiles);
            GetComponentInParent<SceneHandler>().BossScene();
            playerObject.GetComponent<PlayerMovement>().enabled = false;
            playerObject.GetComponent<PlayerAttack>().enabled = false;
            playerObject.GetComponent<PlayerMovement>().body.velocity = new Vector2(0, playerObject.GetComponent<PlayerMovement>().body.velocity.y);
            playerObject.GetComponent<PlayerMovement>().anim.SetBool("run", false);

            StartCoroutine(ActivateBossFight());
        }
    }
    IEnumerator ActivateBossFight()
    {
        backgroundMusic.StartCoroutine(backgroundMusic.StopMusic());
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
        backgroundMusic.BossMusic("icewolf");
        playerObject.GetComponent<PlayerMovement>().enabled = true;
        playerObject.GetComponent<PlayerAttack>().enabled = true;
        icewolfBallObject.GetComponent<IcewolfBallRoute>().body.gravityScale = 1;
    }
    public IEnumerator FrankethTime()
    {
        backgroundMusic.StartCoroutine(backgroundMusic.StopMusic());
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
        backgroundMusic.BossMusic("franketh");
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
        yield return new WaitForSeconds(0.75f);
        tiletrap1.SetActive(true);
        tiletrap2.SetActive(true);
        penguinsCheck = P1;
        penguinsCheck += P2;
        yield return new WaitForSeconds(1.75f);

        checkPenguins = true;
        yield return new WaitUntil(() => bothPenguinsDead);

        yield return new WaitForSeconds(2);
        float redColour = 0;
        float greenColour = 0;
        float blueColour = 1;
        float x = 0;
        for (blueColour = 1; blueColour > 0; blueColour -= 0.005f)
        {
            if (x > 0)
            {
                frankethHolder.transform.position = new Vector3(frankethHolder.transform.position.x, frankethHolder.transform.position.y + 0.1f, frankethHolder.transform.position.z);
                x = 0;
            }
            else
            {
                frankethHolder.transform.position = new Vector3(frankethHolder.transform.position.x, frankethHolder.transform.position.y - 0.1f, frankethHolder.transform.position.z);
                x++;
            }
            redColour += 0.015f;
            blueColour -= 0.015f;
            frankethRend.color = new Color(redColour, 0, blueColour);
            yield return new WaitForSeconds(0.03f);
        }

        frankethAttack.Phase3();
        x = 0;
        for (blueColour = 0; blueColour < 1; blueColour += 0.025f)
        {
            if (x > 0)
            {
                frankethHolder.transform.position = new Vector3(frankethHolder.transform.position.x, frankethHolder.transform.position.y + 0.1f, frankethHolder.transform.position.z);
                x = 0;
            }
            else
            {
                frankethHolder.transform.position = new Vector3(frankethHolder.transform.position.x, frankethHolder.transform.position.y - 0.1f, frankethHolder.transform.position.z);
                x++;
            }
            greenColour += 0.025f;
            frankethRend.color = new Color(redColour, greenColour, blueColour);
            yield return new WaitForSeconds(0.03f);
        }
        frankethRend.color = new Color(1, 1, 1);
        yield return new WaitForSeconds(0.05f);
        frankethObject.GetComponent<FrankethAttack>().enabled = true;
        frankethObject.tag = "Enemy"; 
    }
    private void MoveRightWall()
    {
        rightWall.position = new Vector3(rightWall.position.x + Time.deltaTime * 1 * rightWallSpeed, rightWall.position.y, rightWall.position.z);
    }
    public IEnumerator CodeBroken()
    {
        yield return new WaitForSeconds(1);
        backgroundMusic.BossMusic("victory");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(BlackCanvas());
        StartCoroutine(CodeBrokenSprite());

        yield return new WaitForSeconds(3);

        cameraObject.GetComponent<CameraMovement>().StartCoroutine(cameraObject.GetComponent<CameraMovement>().CodeBroken());
    }
    private IEnumerator BlackCanvas()
    {
        Color c = blackCanvasRend.material.color;
        for (float alpha = 0f; alpha < 0.9; alpha += 0.05f)
        {
            c.a = alpha;
            blackCanvasRend.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(.05f);
        }
    }
    private IEnumerator CodeBrokenSprite()
    {
        Color c = codeBrokenRend.material.color;
        for (float alpha = 0f; alpha < 1; alpha += 0.05f)
        {
            c.a = alpha;
            codeBrokenRend.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(.05f);
        }
    }
    public IEnumerator Death()
    {
        playerHealthBar.SetActive(false);
        death.SetActive(true);

        yield return new WaitForSeconds(7);

        SceneManager.LoadScene("MainMenu");
    }
    public void StopMusic()
    {
        backgroundMusic.StartCoroutine(backgroundMusic.StopMusic());
    }
}
