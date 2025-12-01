using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerUpgrades playerUpgrades;

    [Header("Depth UI")]
    [SerializeField] private Text depthLevelText;
    [SerializeField] private Text depthCostText;

    [Header("Capacity UI")]
    [SerializeField] private Text capacityLevelText;
    [SerializeField] private Text capacityCostText;

    [Header("Spawning")]
    [SerializeField] private FishSpawner fishSpawner;


    void Update()
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        bool isUpgradeMenuState = GameStateManager.Instance.IsState(GameState.UpgradeMenu);

        if (panel.activeSelf != isUpgradeMenuState)
        {
            panel.SetActive(isUpgradeMenuState);

            if (isUpgradeMenuState)
            {
                RefreshUI();
            }
        }
    }

    public void RefreshUI()
    {
        if (playerUpgrades == null)
        {
            return;
        }

        depthLevelText.text = "Depth Lv: " + playerUpgrades.DepthLevel;
        depthCostText.text = "Cost: " + playerUpgrades.GetDepthUpgradeCost();

        capacityLevelText.text = "Capacity Level: " + playerUpgrades.CapacityLevel;
        capacityCostText.text = "Cost: " + playerUpgrades.GetCapacityUpgradeCost();
    }

    public void OnUpgradeDepthButtonPressed()
    {
        if (playerUpgrades == null)
        {
            return;
        }

        if (playerUpgrades.TryUpgradeDepth())
        {
            RefreshUI();
        }
    }

    public void OnUpgradeCapacityButtonPressed()
    {
        if (playerUpgrades == null)
        {
            return;
        }

        if (playerUpgrades.TryUpgradeCapacity())
        {
            RefreshUI();
        }
    }

    public void OnStartNextCastButtonPressed()
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        if (fishSpawner != null)
        {
            fishSpawner.SpawnFishField();
        }

        GameStateManager.Instance.SetState(GameState.Idle);
    }

}
