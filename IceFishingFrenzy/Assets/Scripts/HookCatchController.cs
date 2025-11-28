using System.Collections.Generic;
using UnityEngine;

public class HookCatchController : MonoBehaviour
{
    [Header("Catch Settings")]
    [SerializeField] private int maxCatchCount = 3;

    private List<FishController> caughtFish = new List<FishController>();

    public int MaxCatchCount
    {
        get { return maxCatchCount; }
    }

    public int CaughtFish
    {
        get { return caughtFish.Count; }
    }

    public bool IsFull
    {
        get { return caughtFish.Count >= maxCatchCount; }
    }

    private void OnTriggerEnter2D(Collider2D collider)
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

        FishController fish = collider.GetComponent<FishController>();

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
        List<FishController> copy = new List<FishController>(caughtFish);

        for (int i = 0; i < caughtFish.Count; i++)
        {
            caughtFish.RemoveAt(0);
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

    public void ClearCaughtFish()
    {
        caughtFish.Clear();
    }
}
