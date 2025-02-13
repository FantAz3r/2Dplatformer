using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Money { get; private set; }

    public event Action AmountChange;
    private int _moneyPerCoin = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            Money += _moneyPerCoin;
            AmountChange?.Invoke();
            Destroy(coin.gameObject);
        }
    }
}

