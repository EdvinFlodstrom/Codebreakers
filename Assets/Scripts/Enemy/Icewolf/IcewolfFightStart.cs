using UnityEngine;

public class IcewolfFightStart : MonoBehaviour
{
    [SerializeField] private Behaviour[] components;

    public void InitiateFight()
    {
        foreach (Behaviour item in components)
        {
            item.enabled = true;
        }
    }
}
