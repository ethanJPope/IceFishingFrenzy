using System.Collections.Generic;
using UnityEngine;

public class CatchScoringController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HookCatchController hookCatchController;
    [SerializeField] private PlayerWallet playerWallet;

    private bool hasScoredThisCast = false;

    void Update()
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        if (GameStateManager.Instance.IsState(GameState.Scoring))
        {
            if (!hasScoredThisCast)
            {
                ScoreCurrentCatch();
                hasScoredThisCast = true;

                GameStateManager.Instance.SetState(GameState.UpgradeMenu);
            }
        }
        else
        {
            hasScoredThisCast = false;
        }
    }

    private void ScoreCurrentCatch()
    {
        if (hookCatchController == null || playerWallet == null)
        {
            return;
        }

        List<FishController> caughtFish = hookCatchController.ReleaseCaughtFish();

        int totalValue = 0;

        for (int i = 0; i < caughtFish.Count; i++)
        {
            FishController fish = caughtFish[i];

            if (fish == null)
            {
                continue;
            }

            totalValue += fish.CurrentValue;
            Destroy(fish.gameObject);
        }

        if (totalValue > 0)
        {
            playerWallet.AddCoins(totalValue);
        }

        Debug.Log("Catch scored for " + totalValue + " coins. Total coins: " + playerWallet.CurrentCoins);
    }

}
