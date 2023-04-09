using System.Collections;
using UnityEngine;

public class Scene5Projectiles : MonoBehaviour
{
    [SerializeField] private Transform firepointUp;
    [SerializeField] private Transform firepointDown;

    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private float projectileCooldown;
    private float projectileWait;
    private float layerCounter = 0;

    [System.NonSerialized] public bool shoot = true;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!shoot) return;

        projectileWait += Time.deltaTime;

        if (projectileWait > projectileCooldown)
        {
            projectileWait = 0;
            layerCounter++;
            if (layerCounter > 3)
            {
                layerCounter = 0;
                StartCoroutine(LayerShots());
            }
            else
            {               
                fireballs[RegularProjectile()].transform.position = firepointDown.position;
                fireballs[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();
            }
        }
    }
    private int RegularProjectile()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    IEnumerator LayerShots()
    {
        projectileWait = -2;
        fireballs[RegularProjectile()].transform.position = firepointDown.position;
        fireballs[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        yield return new WaitForSeconds(0.7f);

        fireballs[RegularProjectile()].transform.position = firepointUp.position;
        fireballs[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        yield return new WaitForSeconds(0.7f);

        fireballs[RegularProjectile()].transform.position = firepointDown.position;
        fireballs[RegularProjectile()].GetComponent<FrankethRegularProjectile>().ActivateProjectile();

        projectileWait = 0;
    }
}
