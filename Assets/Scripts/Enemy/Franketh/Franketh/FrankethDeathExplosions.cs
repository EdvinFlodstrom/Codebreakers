
using UnityEngine;

public class FrankethDeathExplosions : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSound;

    void Awake()
    {
        SoundManager.sound.PlaySound(explosionSound);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
