using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// UI Canvas for the Vendor
/// </summary>
public class VendorCanvas : OpenableCanvas<VendorCanvas>, IOpenable
{
    /// <summary> Text displaying the player's current credit amount </summary>
    [SerializeField] private TextMeshProUGUI creditAmountText = null;
    /// <summary> Container of the instantiated VendorStockElements </summary>
    [SerializeField] private Transform stockElementContainer = null;
    /// <summary> VendorStockElement prefab to use to instantiate new elements </summary>
    [SerializeField] private VendorStockElement stockElementPrefab = null;
    
    /// <summary> List of currently instantiated VendorStockElements </summary>
    private List<VendorStockElement> instantiatedStockElements = new();

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }
    
    /// <summary>
    /// Start callback
    /// </summary>
    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// OnEnable callback
    /// </summary>
    private async void OnEnable()
    {
        while (GameManager.Instance == null)
            await UniTask.NextFrame();
        
        GameManager.Instance.GameState.OnCreditAmountChanged += SetCreditAmount;
    }

    /// <summary>
    /// OnDisable callback
    /// </summary>
    private async void OnDisable()
    {
        while (GameManager.Instance == null)
            await UniTask.NextFrame();

        GameManager.Instance.GameState.OnCreditAmountChanged -= SetCreditAmount;
    }

    /// <summary>
    /// Sets up the Canvas with the specified StockElements
    /// </summary>
    /// <param name="stockElements"></param>
    private void Setup(List<InteractionVendor.StockElement> stockElements)
    {
        SetCreditAmount(GameManager.Instance.GameState.CurrentCreditAmount);
        stockElementPrefab.gameObject.SetActive(false);

        // Destroy and clear existing elements
        foreach (var element in instantiatedStockElements)
            Destroy(element.gameObject);
        instantiatedStockElements.Clear();

        // Instantiate and set up each VendorStockElement required
        foreach (var element in stockElements)
        {
            VendorStockElement stockElement = Instantiate(stockElementPrefab, stockElementContainer);
            stockElement.gameObject.SetActive(true);
            stockElement.Setup(element.Name, element.Price);
            instantiatedStockElements.Add(stockElement);
        }
    }

    /// <summary>
    /// Sets the player's current credit amount text
    /// </summary>
    /// <param name="amount">The new amount of credit</param>
    private void SetCreditAmount(int amount)
    {
        creditAmountText.text = "Credits: " + amount;
    }

    /// <summary>
    /// Opens the UI and displays the specified StockElements
    /// </summary>
    /// <param name="stockElements"></param>
    public void Open(List<InteractionVendor.StockElement> stockElements)
    {
        if (IsOpen)
            return;

        Setup(stockElements);
        Open();
    }
}
