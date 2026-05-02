using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuGameNew : MonoBehaviour
{
    [Header("GameObject MenuUI")]
    [SerializeField] private GameObject _uiMenu;
    private void Awake()
    {
        _uiMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Vorremmo attivare il menu");
            if (_uiMenu.activeSelf) return;
            OnClickOpenMenu();
        }
    }
    public void OnClickOpenMenu()
    {
        _uiMenu.SetActive(true);
    }

    public void OnClickResumeButton()
    {
        _uiMenu.SetActive(false);
    }

    public void OnClickExitGame()
    {
        SceneManager.LoadScene("Intro");
    }

}
