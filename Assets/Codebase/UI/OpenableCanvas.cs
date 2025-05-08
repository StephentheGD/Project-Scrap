using DG.Tweening;
using UnityEngine;

/// <summary>
/// Implementation of the singleton Canvas that implements IOpenable
/// </summary>
/// <typeparam name="T">The Type of class to be an Openable Canvas</typeparam>
public class OpenableCanvas<T> : Canvas<T> where T : Singleton<T>, IOpenable
{
    /// <summary> Whether or not the canvas is open </summary>
    [SerializeField] private float transitionTime = 0.5f;
    
    /// <summary> Whether or not the canvas is open </summary>
    public bool IsOpen { get; set; }
    /// <summary> Whether or not the canvas is currently transitioning between being open and closed </summary>
    public bool IsTransitioning { get; set; }
    
    /// <summary>
    /// Opens the canvas, making it visible and interactable
    /// </summary>
    public virtual void Open()
    {
        if (IsOpen)
            return;
        
        IsOpen = true;
        IsTransitioning = true;
        
        // Actually open
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, transitionTime).onComplete += () =>
        {
            canvasGroup.interactable = true;
            IsTransitioning = false;
        };
    }

    
    /// <summary>
    /// Closes the canvas, hiding it from view and making it non-interactive
    /// </summary>
    public virtual void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;
        IsTransitioning = true;
        
        // Actually close
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, transitionTime).onComplete += () =>
        {
            IsTransitioning = false;
        };
    }
}
