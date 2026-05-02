using System.Collections;
using TMPro;
using UnityEngine;

public class UI_ChangeHitValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    private void Awake()
    {
        _text.SetText("");
    }
    public void On_ChangeHit(int damage)
    {
        StartCoroutine(DisplayDamage(damage));
    }


    IEnumerator DisplayDamage(int damage)
    {
        WaitForSeconds wfs = new WaitForSeconds(1.5f);
        _text.SetText(damage.ToString());
        yield return wfs;
        _text.SetText("");
        StopCoroutine(DisplayDamage(damage));
    }
}
