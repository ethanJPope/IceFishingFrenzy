using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int startingCoins = 0;

    public int CurrentCoins { get; private set; }

    private void Awake()
    {
        CurrentCoins = startingCoins;
    }

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;
    }

    public bool CanAfford(int amount)
    {
        return CurrentCoins >= amount;
    }
    public bool TrySpendCoins(int amount)
    {
        if (CanAfford(amount))
        {
            CurrentCoins -= amount;
            return true;
        }
        return false;
    }
}
