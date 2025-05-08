using UnityEngine;

/// <summary>
/// Interactable component that calls Scene Switching related functions
/// </summary>
public class InteractionSwitchScene : MonoBehaviour, IInteractable
{
    /// <summary> The name of the scene that should be loaded </summary>
    [SerializeField] private string sceneName;
    
    #region IInteractable
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip)
    {
        Loader.Instance.SwitchScene(sceneName);
    }
    #endregion
}
