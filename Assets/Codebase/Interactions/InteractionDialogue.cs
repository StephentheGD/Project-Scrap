using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable component that calls Dialogue related functions
/// </summary>
public class InteractionDialogue : MonoBehaviour, IInteractable
{
    /// <summary> List of all dialogue lines to be displayed when the interaction begins
    /// DEBUG: These will be replaced with a localisable dialogue UID </summary>
    [SerializeField] private List<string> DEBUG_DIALOGUES = null;
    
    #region IInteractable
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip)
    {
        DialogueCanvas.Instance.DisplayDialogue(DEBUG_DIALOGUES);
    }
    #endregion
}
