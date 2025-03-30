using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _moneyPerCoin = 1;

    public event Action AmountChange;

    public int Money { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            Money += _moneyPerCoin;
            AmountChange?.Invoke();
        }
    }
}

