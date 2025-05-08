using UnityEngine;

/// <summary>
/// Interface for all interactable components
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip);
}
