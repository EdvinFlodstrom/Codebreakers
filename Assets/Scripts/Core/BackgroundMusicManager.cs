using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float musicVolume;
    private float originalMusicVolume;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip icewolfMusic;
    [SerializeField] private AudioClip frankethMusic;
    [SerializeField] private AudioClip victoryMusic;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        originalMusicVolume = audioSource.volume;

        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
    public void BossMusic(string _music)
    {
        audioSource.volume = originalMusicVolume;
        if (_music == "icewolf")
        {
            audioSource.clip = icewolfMusic;
        }
        else if (_music == "franketh")
        {            
            audioSource.clip = frankethMusic;
        }
        else if (_music == "victory")
        {
            audioSource.loop = false;
            audioSource.clip = victoryMusic;
        }
        audioSource.Play();
    }
    public IEnumerator StopMusic()
    {
        for (musicVolume = audioSource.volume; musicVolume > 0.05; musicVolume -= 0.02f)
        {
            audioSource.volume = musicVolume;
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.clip = null;
    }
}