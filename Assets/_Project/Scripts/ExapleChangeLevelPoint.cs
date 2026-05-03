using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExapleChangeLevelPoint : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(ActivatedFight());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Utility.PlayerTag) &&
           (GameState.Instance.CanIfight))
        {
            GameState.Instance.LastPositionPlayer = other.gameObject.transform;
           
        }
    }
    IEnumerator ActivatedFight()
    {

        yield return new WaitUntil(() => GameState.Instance.CanIfight);
        Debug.Log("Si combatteee");
        yield return new WaitForSeconds(3f);
        GameState.Instance.CanIfight = false;
        SceneManager.LoadScene("Battle");

    }

}


