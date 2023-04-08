using System.Collections;
using UnityEngine;

public class FrankethAttack : MonoBehaviour
{
    [SerializeField] private Scene6Room bossRoom;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform firepoint;
    [SerializeField] private Transform firepointDown;
    [SerializeField] private Transform firepointUp;
    [SerializeField] private Transform firepointHoming;
    [SerializeField] private Transform firepointLaser;
    [SerializeField] private GameObject[] regularProjectiles;
    [SerializeField] private GameObject[] homingProjectiles;
    [SerializeField] private GameObject[] lasers;
    [SerializeField] private GameObject[] deathExplosions;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip laserAttackSound;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float touchingDamage;
    private float attackWait;
    private string attackType;
    private float randomChoice;
    private bool thirdPhase;
    private GameObject playerObject;
    private Transform playerPosition;


    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerObject = playerMovement.gameObject;
    }

    void Update()
    {
        playerPosition = playerMovement.transform;
        attackWait += Time.deltaTime;
        
        if (attackWait > attackCooldown)
        {
            attackWait = 0;
            attackType = RandomChoice();
            Attack(attackType);
        }
    }
    private void Attack(string _type)
    {
        if (_type != "Layer" && _type != "Laser")
        {
            anim.SetTrigger("attack");
            SoundManager.sound.PlaySound(attackSound);
        }

        if (_type == "Regular")
        {
            attackWait = 1;
            regularProjectiles[RegularProjectile()].transform.position = firepointDown.position;
            regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();
        }
        else if (_type == "Homing")
        {
            attackWait = 0;
            homingProjectiles[HomingProjectile()].transform.position = firepointHoming.position;
            homingProjectiles[HomingProjectile()].GetComponent<FrankethHomingProjectile>().ActivateProjectile();
        }
        else if (_type == "Layer")
        {
            if (thirdPhase) attackWait = -2;
            StartCoroutine(LayerProjectile());
        }
        else if (_type == "Laser")
        {
            anim.SetTrigger("laserAttack");
            attackWait = 0;
        }
    }
    private void LaserAttack()
    {
        SoundManager.sound.PlaySound(laserAttackSound);
        lasers[Laser()].transform.position = firepointLaser.position;
        lasers[Laser()].GetComponent<FrankethLaser>().ActivateProjectile(playerPosition);
    }
    private int RegularProjectile()
    {
        for (int i = 0; i < regularProjectiles.Length; i++)
        {
            if (!regularProjectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private int HomingProjectile()
    {
        for (int i = 0; i < homingProjectiles.Length; i++)
        {
            if (!homingProjectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    IEnumerator LayerProjectile()
    {
        anim.SetBool("layerAttack", true);
        SoundManager.sound.PlaySound(attackSound);
        regularProjectiles[RegularProjectile()].transform.position = firepointDown.position;
        regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        yield return new WaitForSeconds(0.5f);

        SoundManager.sound.PlaySound(attackSound);
        regularProjectiles[RegularProjectile()].transform.position = firepointUp.position;
        regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        yield return new WaitForSeconds(0.5f);

        SoundManager.sound.PlaySound(attackSound);
        regularProjectiles[RegularProjectile()].transform.position = firepointDown.position;
        regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        if (thirdPhase)
        {
            yield return new WaitForSeconds(0.5f);

            SoundManager.sound.PlaySound(attackSound);
            regularProjectiles[RegularProjectile()].transform.position = firepointUp.position;
            regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

            yield return new WaitForSeconds(0.5f);

            SoundManager.sound.PlaySound(attackSound);
            regularProjectiles[RegularProjectile()].transform.position = firepointDown.position;
            regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();
        }
        attackWait = 0.5f;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("layerAttack", false);
    }
    private int Laser()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if (!lasers[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Idle()
    {
        anim.SetTrigger("idle");
    }
    private void IdlePhase3()
    {
        anim.SetTrigger("idlePhase3");
    }
    private string RandomChoice()
    {
        if (!thirdPhase) randomChoice = Mathf.Round(Random.Range(0, 6));
        else randomChoice = Mathf.Round(Random.Range(0, 8));

        if (randomChoice == 0 || randomChoice == 1 || randomChoice == 2) attackType = "Regular";
        else if (randomChoice == 3 || randomChoice == 4) attackType = "Layer";
        else if (randomChoice == 5) attackType = "Homing";
        else if (randomChoice == 6 || randomChoice == 7) attackType = "Laser";

        return attackType;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(touchingDamage);
        }
    }
    public void Phase3()
    {
        anim.SetTrigger("phase3");
        attackCooldown = attackCooldown - 0.125f;
        thirdPhase = true;
    }
    IEnumerator FrankethDead()
    {
        playerObject.GetComponent<PlayerHealth>().invulnerable = true;
        yield return new WaitForSeconds(0.3f);
        foreach (var item in deathExplosions)
        {
            item.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        bossRoom.StartCoroutine(bossRoom.CodeBroken());
        anim.SetTrigger("frankethDead");     
    }
}
