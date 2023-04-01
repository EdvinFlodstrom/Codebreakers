using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sound { get; private set; }
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (sound == null)
        {
            sound = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (sound != null && sound != this)
            Destroy(gameObject);
    }
    public void PlaySound(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);
    }
}
