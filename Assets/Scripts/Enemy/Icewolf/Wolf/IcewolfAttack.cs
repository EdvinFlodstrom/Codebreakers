using System.Collections.Generic;
using UnityEngine;

public class IcewolfAttack : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private AudioClip attackSound;
    private float attackCooldown;
    private float attackWait;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        attackCooldown = Mathf.Round(Random.Range(1.5f, 4));
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if (attackWait > attackCooldown) anim.SetTrigger("attack");
    }
    private void Attack()
    {
        SoundManager.sound.PlaySound(attackSound);
        attackCooldown = Mathf.Round(Random.Range(2, 3));
        attackWait = 0;

        projectiles[Projectile()].transform.position = firepoint.position;
        projectiles[Projectile()].GetComponent<IcewolfProjectile>().ProjectileDirection(Mathf.Sign(transform.localScale.x));
    }
    private int Projectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Idle()
    {
        anim.SetTrigger("idle");
    }
}
