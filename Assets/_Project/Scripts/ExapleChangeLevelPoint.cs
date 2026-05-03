using UnityEngine;
using UnityEngine.SceneManagement;
public class ExapleChangeLevelPoint : MonoBehaviour
{
    
    private void Update()
    {
        if (GameState.Instance.CanIfight)
        {
            Debug.Log("Si Combattee");
            GameState.Instance.CanIfight = false;
            ActivatedFight();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Utility.PlayerTag) &&
           (GameState.Instance.CanIfight))
        {
            GameState.Instance.LastPositionPlayer = other.gameObject.transform;
           
        }
    }
    public void ActivatedFight()
    {
        SceneManager.LoadScene("Battle");
        Destroy(gameObject);
    }

}


