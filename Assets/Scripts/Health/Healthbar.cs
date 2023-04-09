using UnityEngine.UI;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Image totalHeart1;
    [SerializeField] private Image totalHeart2;
    [SerializeField] private Image totalHeart3;
    [SerializeField] private Image totalHeart4;

    [SerializeField] private Image[] currentHearts;

    private float heartsLost = 0;
    private float heartsGained;


    void Awake()
    {
        if (PlayerPrefs.GetInt("PlayerMaxHealth") != 1)
        {
            totalHeart4.enabled = false;
            currentHearts[3].enabled = false;
        }           
    }
        
    void Update()
    {
        heartsGained = heartsLost;
        heartsLost = playerHealth.maxHealth - playerHealth.currentHealth;
        

        if (heartsLost > 0)
        {
            RemoveHeart(heartsLost);
        }
        if (heartsGained != heartsLost)
        {
            AddHeart(heartsLost + 1);
        }
    }
    private void RemoveHeart(float _hearts)
    {
         if (playerHealth.maxHealth > 3)
        {
            if (_hearts == 1) currentHearts[3].enabled = false;
            if (_hearts == 2) currentHearts[2].enabled = false;
            if (_hearts == 3) currentHearts[1].enabled = false;
            if (_hearts == 4) currentHearts[0].enabled = false;
        }
         else
        {
            if (_hearts == 1) currentHearts[2].enabled = false;
            if (_hearts == 2) currentHearts[1].enabled = false;
            if (_hearts == 3) currentHearts[0].enabled = false;
        }
    }
    private void AddHeart(float _hearts)
    {
        if (playerHealth.maxHealth > 3)
        {
            if (_hearts == 1) currentHearts[3].enabled = true;
            if (_hearts == 2) currentHearts[2].enabled = true;
            if (_hearts == 3) currentHearts[1].enabled = true;
            if (_hearts == 4) currentHearts[0].enabled = true;
        }
        else
        {
            if (_hearts == 1) currentHearts[2].enabled = true;
            if (_hearts == 2) currentHearts[1].enabled = true;
            if (_hearts == 3) currentHearts[0].enabled = true;
        }
    }
    public void FourthHeart()
    {
        totalHeart4.enabled = true;
        foreach (Image item in currentHearts)
        {
            item.enabled = true;
        }
    }
}
