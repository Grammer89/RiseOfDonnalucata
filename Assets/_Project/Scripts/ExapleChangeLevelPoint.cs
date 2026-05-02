using UnityEngine;
using UnityEngine.SceneManagement;

public class ExapleChangeLevelPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Utility.PlayerTag))
        {
            SceneManager.LoadScene("Battle");
        }
    }
}
