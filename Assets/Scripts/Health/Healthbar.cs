using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Image totalHeart1;
    [SerializeField] private Image totalHeart2;
    [SerializeField] private Image totalHeart3;
    [SerializeField] private Image totalHeart4;

    [SerializeField] private Image currentHeart1;
    [SerializeField] private Image currentHeart2;
    [SerializeField] private Image currentHeart3;
    [SerializeField] private Image currentHeart4;

    private float heartsLost = 0;
    private float heartsGained;


    void Start()
    {
        totalHeart4.enabled = false;
        currentHeart4.enabled = false;
    }
        
    void Update()
    {
        heartsGained = heartsLost;
        heartsLost = playerHealth.startingHealth - playerHealth.currentHealth;
        

        if (heartsLost > 0)
        {
            removeHeart(heartsLost);
        }
        if (heartsGained != heartsLost)
        {
            addHeart(heartsLost + 1);
        }

        if (playerHealth.maxHealth > 3) totalHeart4.enabled = true;
    }
    private void removeHeart(float _hearts)
    {
         if (playerHealth.maxHealth > 3)
        {
            if (_hearts == 1) currentHeart4.enabled = false;
            if (_hearts == 2) currentHeart3.enabled = false;
            if (_hearts == 3) currentHeart2.enabled = false;
            if (_hearts == 4) currentHeart1.enabled = false;
        }
         else
        {
            if (_hearts == 1) currentHeart3.enabled = false;
            if (_hearts == 2) currentHeart2.enabled = false;
            if (_hearts == 3) currentHeart1.enabled = false;
        }
    }
    private void addHeart(float _hearts)
    {
        if (playerHealth.maxHealth > 3)
        {
            if (_hearts == 1) currentHeart4.enabled = true;
            if (_hearts == 2) currentHeart3.enabled = true;
            if (_hearts == 3) currentHeart2.enabled = true;
            if (_hearts == 4) currentHeart1.enabled = true;
        }
        else
        {
            if (_hearts == 1) currentHeart3.enabled = true;
            if (_hearts == 2) currentHeart2.enabled = true;
            if (_hearts == 3) currentHeart1.enabled = true;
        }
    }
}
