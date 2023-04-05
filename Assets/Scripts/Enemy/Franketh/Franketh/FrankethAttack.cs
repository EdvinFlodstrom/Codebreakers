using System.Collections;
using UnityEngine;

public class FrankethAttack : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private Transform firepointDown;
    [SerializeField] private Transform firepointUp;
    [SerializeField] private GameObject[] regularProjectiles;
    [SerializeField] private GameObject[] homingProjectiles;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float touchingDamage;
    private float attackWait;
    private string attackType;
    private float randomChoice;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attackWait += Time.deltaTime;
        
        if (attackWait > attackCooldown)
        {
            attackType = RandomChoice();
            Attack(attackType);
        }
    }
    private void Attack(string _type)
    {
        SoundManager.sound.PlaySound(attackSound);
        if (_type != "Layer")
        {
            attackWait = 0;
            anim.SetTrigger("attack");
        }

        if (_type == "Regular")
        {
            regularProjectiles[RegularProjectile()].transform.position = firepoint.position;
            regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();
        }
        else if (_type == "Homing")
        {
            homingProjectiles[HomingProjectile()].transform.position = firepoint.position;
            homingProjectiles[HomingProjectile()].GetComponent<FrankethHomingProjectile>().ActivateProjectile();
        }
        else if (_type == "Layer")
        {
            StartCoroutine(LayerProjectile());
        }
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
        homingProjectiles[RegularProjectile()].transform.position = firepointDown.position;
        regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        yield return new WaitForSeconds(0.75f);

        homingProjectiles[RegularProjectile()].transform.position = firepointUp.position;
        regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        yield return new WaitForSeconds(0.75f);

        homingProjectiles[RegularProjectile()].transform.position = firepointDown.position;
        regularProjectiles[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        attackWait = 0;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("layerAttack", false);
    }
    private void Idle()
    {
        anim.SetTrigger("idle");
    }
    private string RandomChoice()
    {
        randomChoice = Mathf.Round(Random.Range(0, 6));

        if (randomChoice == 0 || randomChoice == 1 || randomChoice == 2) attackType = "Regular";
        else if (randomChoice == 3 || randomChoice == 4) attackType = "Layer";
        else if (randomChoice == 5) attackType = "Homing";

        return attackType;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(touchingDamage);
        }
    }
}
