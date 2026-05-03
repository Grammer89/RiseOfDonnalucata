using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class ExampleMerchant : MonoBehaviour
{
    [SerializeField] private SO_Merchant _merchant;
    [SerializeField] private UI_MerchantWindow _merchantWindow;
    [SerializeField] GameObject _merchantCanvas;

    private void Awake()
    {
        _merchantCanvas.SetActive(false);
        _merchantWindow.Setup(_merchant);
    }

    private void Update()
    {
        if (GameState.Instance.OpenItemShop)
        {
            GameState.Instance.OpenItemShop = false;
            StartCoroutine(ActiveItemShop());
        }

    }

    IEnumerator ActiveItemShop()
    {
        WaitForSeconds yfs = new WaitForSeconds(0.5f);

        yield return yfs;
        _merchantCanvas.SetActive(true);
        _merchantWindow.Setup(_merchant);
        StopCoroutine(ActiveItemShop());

    }
}
