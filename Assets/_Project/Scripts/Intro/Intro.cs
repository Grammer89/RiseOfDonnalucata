using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
}
