using UnityEngine;

/// <summary>
/// Interactable component that calls Bed related functions
/// </summary>
public class InteractionBed : MonoBehaviour, IInteractable
{
    #region IInteractable
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip)
    {
        // TODO: When Time is implemented, advance one day
    }
    #endregion
}
