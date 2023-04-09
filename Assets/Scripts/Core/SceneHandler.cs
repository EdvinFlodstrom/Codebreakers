using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] sceneList;

    public void SceneNumber(int _num)
    {
        if (_num == 2) LoadScene3();
        if (_num == 3) LoadScene4();
        if (_num == 4) LoadScene5();
        if (_num == 5) LoadScene6();
    }
    private void LoadScene3()
    {
        sceneList[2].SetActive(true);
    }
    private void LoadScene4()
    {
        sceneList[0].SetActive(false);
        sceneList[3].SetActive(true);
    }
    private void LoadScene5()
    {
        sceneList[1].SetActive(false);
        sceneList[4].SetActive(true);
    }
    private void LoadScene6()
    {
        sceneList[2].SetActive(false);
        sceneList[5].SetActive(true);
    }
    public void BossScene()
    {
        sceneList[3].SetActive(false);
        sceneList[4].SetActive(false);
    }
}
