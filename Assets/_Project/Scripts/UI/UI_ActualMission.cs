using UnityEngine;
using TMPro;

public class UI_ActualMission : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMissionUI;
  public void ModifyMissionUI(string text)
    {
        _textMissionUI.SetText(text);
    }    

}
