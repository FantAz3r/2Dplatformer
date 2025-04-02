using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action AmountChange;

    public int Money { get; private set; }

    public void AddMoney(int amount)
    {
        Money += amount;
        AmountChange?.Invoke(); 
    }
}

