using UnityEngine;

/// <summary>
/// Interactable component that calls Scrap Refiner related functions
/// </summary>
public class InteractionScrapRefiner : MonoBehaviour, IInteractable
{
    #region IInteractable
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip)
    {
        ip.enabled = false;

        int refinedCredits = GameManager.Instance.GameState.CurrentScrapAmount * 3;
        GameManager.Instance.GameState.SetScrapAmount(0);
        GameManager.Instance.GameState.AddCreditAmount(refinedCredits);
    }
    #endregion
}
