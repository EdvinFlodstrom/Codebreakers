using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Play()
    {
        PlayerPrefs.SetInt("CheckpointNumber", 0);
        PlayerPrefs.SetInt("PlayerMaxHealth", 0);
        SceneManager.LoadScene("Tutorial");
    }
}
