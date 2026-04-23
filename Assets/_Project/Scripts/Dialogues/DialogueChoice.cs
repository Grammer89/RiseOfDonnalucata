using TMPro;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    public TextMeshProUGUI _choiceText;
    public int _choiceIndex;

    // Update is called once per frame
    public void OnChoiceClicked()
    {
      //  DialogueManager.Instance.MakeChoice(_choiceIndex);
    }
}
