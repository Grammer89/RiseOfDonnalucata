using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class ExampleMerchant : MonoBehaviour
{
    [SerializeField] private SO_Merchant _merchant;
    [SerializeField] private UI_MerchantWindow _merchantWindow;
    [SerializeField] GameObject _merchantCanvas;

    private bool _playerInRange;

    private void Awake()
    {
        _merchantCanvas.SetActive(false);
        _merchantWindow.
            Setup(_merchant);
    }

    private void Update()
    {
        if (!_playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _merchantCanvas.SetActive(true);
            _merchantWindow.Setup(_merchant);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utility.PlayerTag))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Utility.PlayerTag))
        {
            _playerInRange = false;
        }
    }
}
