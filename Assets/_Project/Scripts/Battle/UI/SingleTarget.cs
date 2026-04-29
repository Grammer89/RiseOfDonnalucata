using TMPro;
using UnityEngine;

public class SingleTarget : MonoBehaviour
{
    [Header("Setting Text Name HP MP")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _mpText;

    public string GetNameText() => _nameText.text;



    public void SetName(string name)
    {
        _nameText.SetText(name);
    }

    public void SetHp(int hpActual, int hpMax)
    {
        _hpText.SetText(hpActual.ToString() + "/" + hpMax.ToString() + " HP");
    }
    public void SetMp(int mpActual, int mpMax)
    {
        _mpText.SetText(mpActual.ToString() + "/" + mpMax.ToString() + " MP");
    }
}
