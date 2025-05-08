using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// UI Canvas for the Scrapheap Status
/// </summary>
public class ScrapheapStatusCanvas : Canvas<ScrapheapStatusCanvas>
{
    /// <summary> Text displaying the amount of scrap collected</summary>
    [SerializeField] private TextMeshProUGUI text = null;

    /// <summary>
    /// Awake callback
    /// </summary>
    public void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start callback
    /// </summary>
    private async void Start()
    {
        // TODO: Replace this with a boolean isInitialised, and a callback OnInitialisation in GameManager
        // Wait until the GameManager is available
        while (GameManager.Instance == null)
            await UniTask.NextFrame();
        
        // Wait until the GameState is available
        while (GameManager.Instance.GameState == null)
            await UniTask.NextFrame();
        
        SetScrapAmount(GameManager.Instance.GameState.CurrentScrapAmount);
    }

    /// <summary>
    /// OnEnable callback
    /// </summary>
    private async void OnEnable()
    {
        while (GameManager.Instance == null)
            await UniTask.NextFrame();
        
        GameManager.Instance.GameState.OnScrapAmountChanged += SetScrapAmount;
    }

    /// <summary>
    /// OnDisable callback
    /// </summary>
    private async void OnDisable()
    {
        while (GameManager.Instance == null)
            await UniTask.NextFrame();

        GameManager.Instance.GameState.OnScrapAmountChanged -= SetScrapAmount;
    }

    /// <summary>
    /// Sets the visible text to the specified scrap amount
    /// </summary>
    /// <param name="amount">The new amount of scrap the player has</param>
    private void SetScrapAmount(int amount)
    {
        text.text = "Scrap: " + amount;
    }
}
