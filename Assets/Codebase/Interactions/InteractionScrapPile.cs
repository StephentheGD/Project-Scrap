using UnityEngine;

/// <summary>
/// Interactable component that calls Scrap Pile related functions
/// </summary>
public class InteractionScrapPile : MonoBehaviour, IInteractable
{
    /// <summary> GameObject acting as the parent for all visuals on the object </summary>
    [SerializeField] private GameObject visuals;

    #region IInteractable
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip)
    {
        visuals.SetActive(false);
        ip.enabled = false;

        int scrapAmount = Random.Range(3, 7);
        GameManager.Instance.GameState.AddScrapAmount(scrapAmount);
    }
    #endregion
}
