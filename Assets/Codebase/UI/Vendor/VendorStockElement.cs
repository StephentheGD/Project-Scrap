using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Element displaying a single Vendor Stock element
/// </summary>
public class VendorStockElement : MonoBehaviour
{
    /// <summary> Text displaying the item's name </summary>
    [SerializeField] private TextMeshProUGUI itemText = null;
    /// <summary> Text displaying the item's price </summary>
    [SerializeField] private TextMeshProUGUI priceText = null;
    /// <summary> Button component on this object </summary>
    [SerializeField] private Button button = null;

    /// <summary> Cached price value </summary>
    private int price = 0;
    
    /// <summary>
    /// OnEnable callback
    /// </summary>
    private async void OnEnable()
    {
        while (GameManager.Instance == null)
            await UniTask.NextFrame();
        
        GameManager.Instance.GameState.OnCreditAmountChanged += UpdateButton;
    }

    /// <summary>
    /// OnDisable callback
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.GameState.OnCreditAmountChanged -= UpdateButton;
    }

    /// <summary>
    /// Sets up the UI element with the specified information
    /// </summary>
    /// <param name="item"></param>
    /// <param name="price"></param>
    public async void Setup(string item, int price)
    {
        itemText.text = item;
        priceText.text = "Credits: " + price;

        this.price = price;
        
        // Ensure the GameManager is initialised
        while (GameManager.Instance == null)
            await UniTask.NextFrame();

        UpdateButton(GameManager.Instance.GameState.CurrentCreditAmount);
    }

    /// <summary>
    /// Purchases the item and removes the price from the player's current credit amount
    /// </summary>
    public void Purchase()
    {
        // Remove Credits
        GameManager.Instance.GameState.AddCreditAmount(-price);
        
        // Add item to inventory
        // TODO: When adding Items, add logic here to remove the item from the players inventory
    }

    /// <summary>
    /// Updates the button's interactable state to the appropriate value
    /// </summary>
    private void UpdateButton(int currentCreditAmount)
    {
        button.interactable = CanPurchase(currentCreditAmount);
    }

    /// <summary>
    /// Checks whether the player has the required amount of credits to purchase the item
    /// </summary>
    /// <param name="currentCreditAmount">The player's current credit amount</param>
    /// <returns>Whether or not the player can purchase the item</returns>
    private bool CanPurchase(int currentCreditAmount)
         => currentCreditAmount >= price;
}
