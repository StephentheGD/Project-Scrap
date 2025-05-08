using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Allows an actor to call Interact from an IInteractable component
/// </summary>
public class InteractionPoint : MonoBehaviour
{
    /// <summary> List of all currently loading InteractionPoints </summary>
    public static List<InteractionPoint> Instances = new();
    
    /// <summary> GameObject containing visuals of the prompt for interaction </summary>
    [SerializeField] private GameObject prompt = null;

    /// <summary> Whether or not the prompt is currently being displayed </summary>
    private bool isDisplayingPrompt = false;

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        prompt.transform.localScale = Vector3.zero;
        isDisplayingPrompt = false;
    }

    /// <summary>
    /// OnEnable callback
    /// </summary>
    private void OnEnable()
    {
        Instances.Add(this);
    }

    /// <summary>
    /// OnDisable callback
    /// </summary>
    private void OnDisable()
    {
        Instances.Remove(this);
        HidePrompt();
    }

    /// <summary>
    /// Displays the prompt visuals
    /// </summary>
    /// <param name="force">Whether or not to request displaying the prompt regardless of whether the prompt is already
    /// being displayed </param>
    public void DisplayPrompt(bool force = false)
    {
        if (isDisplayingPrompt && !force)
            return;

        isDisplayingPrompt = true;
        prompt.transform.DOKill();
        // Actually display prompt
        prompt.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).onComplete +=
            () =>
            {
                prompt.transform.localScale = Vector3.one;
            };
    }

    /// <summary>
    /// Hides the prompt visuals
    /// </summary>
    /// <param name="force">Whether or not to request hiding the prompt regardless of whether the prompt is already
    /// being displayed </param>
    public void HidePrompt(bool force = false)
    {
        if (!isDisplayingPrompt && !force)
            return;

        isDisplayingPrompt = false;
        prompt.transform.DOKill();
        // Actually hide prompt
        prompt.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).onComplete +=
            () =>
            {
                prompt.transform.localScale = Vector3.zero;
            };
    }

    /// <summary>
    /// Calls Interact on the IInteractable on the same object as this component
    /// </summary>
    public void Interact()
    {
        GetComponent<IInteractable>()?.Interact(this);
    }
}
