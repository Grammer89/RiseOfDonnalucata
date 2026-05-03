using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ComingSoon : MonoBehaviour
{
    [Header("GameObject MenuUI")]
    [SerializeField] private GameObject _uiComingSoon;
    private void Awake()
    {
        _uiComingSoon.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(ActiveComingSoon());
    }
    IEnumerator ActiveComingSoon()
    {
        yield return new WaitUntil(() => GameState.Instance.ComingSoon);
        GameState.Instance.ResetSave = true;
        yield return new WaitForSeconds(2f);
        GameState.Instance.ComingSoon = false;
       _uiComingSoon.SetActive(true);
        StopCoroutine(ActiveComingSoon());
    }
  
  
    public void OnClickExitGame()
    {
        SceneManager.LoadScene("Intro");
    }
}
