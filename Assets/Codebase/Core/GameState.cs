using UnityEngine;

/// <summary>
/// Stores all data for and handles all events for the current game state
/// </summary>
public class GameState
{
    #region Scrap
    /// <summary> Public accessor for currentScrapAmount </summary>
    public int CurrentScrapAmount => currentScrapAmount;
    /// <summary> The current amount of scrap </summary>
    private int currentScrapAmount = 0;

    /// <summary>
    /// Sets the current scrap amount
    /// </summary>
    /// <param name="amount">The new value of currentScrapAmount</param>
    public void SetScrapAmount(int amount)
    {
        currentScrapAmount = amount;
        OnScrapAmountChanged?.Invoke(currentScrapAmount);
    }

    /// <summary>
    /// Modifies the current scrap amount by the specified amount
    /// </summary>
    /// <param name="delta">The change in the current scrap amount</param>
    public void AddScrapAmount(int delta)
    {
        currentScrapAmount += delta;
        OnScrapAmountChanged?.Invoke(currentScrapAmount);
    }
    
    /// <summary> Delegate to be used when the scrap amount changes, returning the new amount </summary>
    public delegate void ScrapAmountChangedEvent(int newAmount);
    /// <summary> Callback for when the scrap amount changes </summary>
    public event ScrapAmountChangedEvent OnScrapAmountChanged;
    #endregion
    
    #region Credit
    /// <summary> Public accessor for currentCreditAmount </summary>
    public int CurrentCreditAmount => currentCreditAmount;
    /// <summary> The current amount of scrap </summary>
    private int currentCreditAmount = 0;

    /// <summary>
    /// Sets the current credit amount
    /// </summary>
    /// <param name="amount">The new value of currentCreditAmount</param>
    public void SetCreditAmount(int amount)
    {
        currentCreditAmount = amount;
        OnCreditAmountChanged?.Invoke(currentCreditAmount);
    }

    /// <summary>
    /// Modifies the current credit amount by the specified amount
    /// </summary>
    /// <param name="delta">The change in the current credit amount</param>
    public void AddCreditAmount(int delta)
    {
        currentCreditAmount += delta;
        OnCreditAmountChanged?.Invoke(currentCreditAmount);
    }
    
    /// <summary> Delegate to be used when the credit amount changes, returning the new amount </summary>
    public delegate void CreditAmountChangedEvent(int newAmount);
    /// <summary> Callback for when the credit amount changes </summary>
    public event CreditAmountChangedEvent OnCreditAmountChanged;
    #endregion
}

