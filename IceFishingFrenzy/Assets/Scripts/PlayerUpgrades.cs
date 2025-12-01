using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerWallet playerWallet;

    [Header("Depth Upgrade")]
    [SerializeField] private int depthLevel = 0;
    [SerializeField] private float baseMaxDepth = -15f;
    [SerializeField] private float depthPerLevel = -5f;
    [SerializeField] private int baseDepthCost = 10;
    [SerializeField] private int depthCostIncrease = 5;

    [Header("Capacity Upgrade")]
    [SerializeField] private int capacityLevel = 0;
    [SerializeField] private int baseCapacity = 3;
    [SerializeField] private int capacityPerLevel = 1;
    [SerializeField] private int baseCapacityCost = 15;
    [SerializeField] private int capacityCostIncrease = 10;

    public int DepthLevel
    {
        get { return depthLevel; }
    }

    public int CapacityLevel
    {
        get { return capacityLevel; }
    }

    public float GetMaxDepth()
    {
        return baseMaxDepth + depthPerLevel * depthLevel;
    }

    public int GetCatchCapacity()
    {
        return baseCapacity + capacityPerLevel * capacityLevel;
    }

    public int GetDepthUpgradeCost()
    {
        return baseDepthCost + depthCostIncrease * depthLevel;
    }

    public int GetCapacityUpgradeCost()
    {
        return baseCapacityCost + capacityCostIncrease * capacityLevel;
    }


    public bool TryUpgradeDepth()
    {
        if (playerWallet == null)
        {
            return false;
        }

        int cost = GetDepthUpgradeCost();

        if (!playerWallet.TrySpendCoins(cost))
        {
            return false;
        }

        depthLevel += 1;
        return true;
    }

    public bool TryUpgradeCapacity()
    {
        if (playerWallet == null)
        {
            return false;
        }

        int cost = GetCapacityUpgradeCost();

        if (!playerWallet.TrySpendCoins(cost))
        {
            return false;
        }

        capacityLevel += 1;
        return true;
    }
}
