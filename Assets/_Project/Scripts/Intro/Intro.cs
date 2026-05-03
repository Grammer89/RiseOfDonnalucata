using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _info;

    public void Awake()
    {
        _info.SetActive(false);
        _game.SetActive(true);
    }
    public void OnClickNewGame()
    {
   
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void OnClickInfo()
    {
        _game.SetActive(false);
        _info.SetActive(true);
    }

    public void OnResume()
    {
        _info.SetActive(false);
        _game.SetActive(true);
    }

    public void ResetGame()
    {

    }
}
