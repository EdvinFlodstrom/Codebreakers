using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [SerializeField] private Transform laserTop;
    [SerializeField] private GameObject laser;
    
    [SerializeField] private float laserActiveDuration;
    private float currentLaserActiveDuration = 0;

    [SerializeField] private float laserNotActiveDuration;
    private float currentLaserNotActiveDuration = 0;

    private bool laserActive;

    private Animator anim;
    private Animator otherAnim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        otherAnim = laserTop.GetComponent<Animator>();
    }

    void Update()
    {
        if (!laserActive) currentLaserNotActiveDuration += Time.deltaTime;
        if (laserActive) currentLaserActiveDuration += Time.deltaTime;

        if (currentLaserNotActiveDuration > laserNotActiveDuration)
        {
            currentLaserNotActiveDuration = 0;
            laserActive = true;
            anim.SetBool("active", true);
            otherAnim.SetBool("active", true);
            ActivateLaser();
        }
        if (currentLaserActiveDuration > laserActiveDuration)
        {
            currentLaserActiveDuration = 0;
            laserActive = false;
            anim.SetBool("active", false);
            otherAnim.SetBool("active", false);
            DeactivateLaser();
        }
    }
    private void ActivateLaser()
    {
        laser.SetActive(true);
    }
    private void DeactivateLaser()
    {
        laser.SetActive(false);
    }
}
