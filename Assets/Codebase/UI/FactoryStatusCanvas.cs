using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// UI Canvas for the Factory Status
/// </summary>
public class FactoryStatusCanvas : Canvas<FactoryStatusCanvas>
{
    /// <summary> Text displaying the player's current scrap amount </summary>
    [SerializeField] private TextMeshProUGUI scrapAmountText;
    /// <summary> Text displaying the player's current credit amount </summary>
    [SerializeField] private TextMeshProUGUI creditAmountText;

    /// <summary>
    /// OnEnable callback
    /// </summary>
    public async void OnEnable()
    {
        while (GameManager.Instance == null)
            await UniTask.NextFrame();

        GameManager.Instance.GameState.OnScrapAmountChanged += UpdateScrapAmountText;
        GameManager.Instance.GameState.OnCreditAmountChanged += UpdateCreditAmountText;

        UpdateScrapAmountText(GameManager.Instance.GameState.CurrentScrapAmount);
        UpdateCreditAmountText(GameManager.Instance.GameState.CurrentCreditAmount);
    }

    /// <summary>
    /// OnDisable callback
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.GameState.OnScrapAmountChanged -= UpdateScrapAmountText;
        GameManager.Instance.GameState.OnCreditAmountChanged -= UpdateCreditAmountText;
    }

    /// <summary>
    /// Updates the scrap amount text
    /// </summary>
    /// <param name="scrapAmount">The player's current scrap amount</param>
    private void UpdateScrapAmountText(int scrapAmount)
    {
        scrapAmountText.text = "Scrap: " + scrapAmount;
    }

    /// <summary>
    /// Updates the credit amount text
    /// </summary>
    /// <param name="creditAmount">The player's current credit amount</param>
    private void UpdateCreditAmountText(int creditAmount)
    {
        creditAmountText.text = "Credit: " + creditAmount;
    }
}
