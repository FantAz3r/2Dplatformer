using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]

public class Collector : MonoBehaviour, ICollector
{
    public void Collect(Coin coin)
    {
        if(TryGetComponent(out Wallet wallet))
        {
            wallet.AddMoney(coin.MoneyPerCoin);
        }
    }

    public void Collect(MedKit medKit)
    {
        if (TryGetComponent(out Health health))
        {
            health.Heal(medKit.HealAmount); 
        }
    }
}
