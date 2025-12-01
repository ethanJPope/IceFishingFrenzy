using System.Collections.Generic;
using UnityEngine;

public class HookCatchController : MonoBehaviour
{
    [Header("Catch Settings")]
    [SerializeField] private int defaultMaxCatchCount = 3;

    [Header("Upgrades")]
    [SerializeField] private PlayerUpgrades playerUpgrades;

    private List<FishController> caughtFish = new List<FishController>();

    public int MaxCatchCount
    {
        get { return GetCurrentMaxCatchCount(); }
    }

    public int CurrentCatchCount
    {
        get { return caughtFish.Count; }
    }

    public bool IsFull
    {
        get { return caughtFish.Count >= GetCurrentMaxCatchCount(); }
    }

    private int GetCurrentMaxCatchCount()
    {
        if (playerUpgrades != null)
        {
            return playerUpgrades.GetCatchCapacity();
        }

        return defaultMaxCatchCount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        if (!GameStateManager.Instance.IsState(GameState.Reeling))
        {
            return;
        }

        if (IsFull)
        {
            return;
        }

        FishController fish = other.GetComponent<FishController>();

        if (fish == null)
        {
            return;
        }

        if (caughtFish.Contains(fish))
        {
            return;
        }

        CatchFish(fish);
    }

    private void CatchFish(FishController fish)
    {
        fish.AttachToHook(transform);

        caughtFish.Add(fish);

        if (IsFull)
        {
            HandleHookFull();
        }
    }

    private void HandleHookFull()
    {
        
    }

    public List<FishController> GetCaughtFishCopy()
    {
        List<FishController> copy = new List<FishController>();

        for (int i = 0; i < caughtFish.Count; i++)
        {
            copy.Add(caughtFish[i]);
        }

        return copy;
    }

    public List<FishController> ReleaseCaughtFish()
    {
        List<FishController> released = new List<FishController>();

        for (int i = 0; i < caughtFish.Count; i++)
        {
            released.Add(caughtFish[i]);
        }

        caughtFish.Clear();

        return released;
    }

    public void ClearCaughtFishWithoutReturning()
    {
        caughtFish.Clear();
    }
}
