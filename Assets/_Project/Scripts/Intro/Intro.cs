using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private const string _saveVariablesKey = "INK_VARIABLES";
    private const string _nameMissionId = "MissionId";
    private const string _missionCompleted = "MissionCompleted";
    private const string _openItemShop = "OpenItemShop";
    private const string _CanIfight = "CanIfight";
    public void OnClickNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
}
