using TMPro;
using UnityEngine;

public class WalletViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.AmountChange += DisplayAmount;
        DisplayAmount();
    }

    private void OnDisable()
    {
        _wallet.AmountChange -= DisplayAmount;
    }

    private void DisplayAmount()
    {
        _amountText.text = _wallet.Money.ToString();
    }
}
