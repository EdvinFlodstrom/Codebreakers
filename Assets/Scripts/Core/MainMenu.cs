using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button beginButton;
    public Button testButton;

    private void Start()
    {
        //beginButton.onClick.AddListener(Begin);
        //testButton.onClick.AddListener(Test);
    }
    public void Quit()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Play()
    {

    }
}
