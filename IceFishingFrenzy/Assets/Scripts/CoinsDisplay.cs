using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerWallet playerWallet;
    [SerializeField] private Text coinsText;

    void Start()
    {
        UpdateCoinsText();
    }

    void Update()
    {
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        if (playerWallet == null || coinsText == null)
        {
            return;
        }

        coinsText.text = "Coins: " + playerWallet.CurrentCoins.ToString();
    }
}
