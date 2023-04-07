using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class BossHealthbar : MonoBehaviour
{
    [SerializeField] private Scene6Room bossRoom;

    [Header("Icewolf healthbar")]
    [SerializeField] private GameObject icewolfHealthbar;
    [SerializeField] private EnemyHealth icewolfHealth;
    [SerializeField] private EnemyHealth ballHealth;

    [SerializeField] private Image icewolfHealthbarFull;
    [SerializeField] private Image icewolfHealthbarEmpty;

    [SerializeField] private Image icewolfName;

    [Header("Franketh healthbar")]
    [SerializeField] private GameObject frankethHealthbar;
    [SerializeField] private EnemyHealth frankethHealth;

    [SerializeField] private Image frankethHealthbarFull;
    [SerializeField] private Image frankethHealthbarEmpty;

    [SerializeField] private Image frankethName;

    private float icewolfMaxHealth;
    private float icewolfCurrentHealth;

    private float frankethMaxHealth;
    private float frankethCurrentHealth;

    private bool icewolfDead;
    private bool frankethDead;
    private bool frankethPhase2;
    

    void Update()
    {
        if (!icewolfHealthbar.activeInHierarchy && !frankethHealthbar.activeInHierarchy) return;

        if (icewolfHealthbar.activeInHierarchy)
        {
            icewolfCurrentHealth = icewolfHealth.enemyCurrentHealth + ballHealth.enemyCurrentHealth;
            icewolfHealthbarFull.fillAmount = icewolfCurrentHealth / icewolfMaxHealth;
            if (icewolfCurrentHealth < 1 && !icewolfDead)
            {
                icewolfDead = true;
                StartCoroutine(DeactivateIcewolf());
            }
        }
        if (frankethHealthbar.activeInHierarchy)
        {
            frankethCurrentHealth = frankethHealth.enemyCurrentHealth;
            frankethHealthbarFull.fillAmount = frankethCurrentHealth / frankethMaxHealth;
            if (frankethCurrentHealth <= frankethMaxHealth / 2 && !frankethPhase2)
            {
                frankethPhase2 = true;
                bossRoom.StartCoroutine(bossRoom.FrankethSecondPhase());
            }
            if (frankethCurrentHealth < 1 && !frankethDead)
            {
                frankethDead = true;
                StartCoroutine(DeactivateFranketh());
            }
        }
    }
    public void ActivateIcewolf()
    {
        icewolfMaxHealth = icewolfHealth.enemyCurrentHealth + ballHealth.enemyCurrentHealth;
        icewolfCurrentHealth = icewolfMaxHealth;
        icewolfHealthbar.SetActive(true);
    }
    IEnumerator DeactivateIcewolf()
    {
        yield return new WaitForSeconds(1.5f);
        icewolfHealthbar.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        bossRoom.StartCoroutine(bossRoom.FrankethTime());
    }
    public void ActivateFranketh()
    {
        frankethMaxHealth = frankethHealth.enemyCurrentHealth;
        frankethHealthbar.SetActive(true);
    }
    IEnumerator DeactivateFranketh()
    {
        yield return new WaitForSeconds(1);
        frankethHealthbar.SetActive(false);
    }
}
