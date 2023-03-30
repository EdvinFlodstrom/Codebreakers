using System.Collections.Generic;
using UnityEngine;

public class IcewolfAttack : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] projectiles;
    [Header("Minimum attack cooldown of 1.5s")]
    [SerializeField] private float[] attackCooldownList;
    private float attackCooldown;
    private float attackWait;

    void Awake()
    {
        //attackCooldown = 
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if (attackCooldown > attackWait) Attack();
    }
    private void Attack()
    {
        Debug.Log(attackCooldown);
        attackWait = 0;
        attackCooldown = Random.Range(attackCooldownList[0], attackCooldownList.Length);

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
}
