using System;
using UnityEngine;

/// <summary>
/// Singleton Canvas class to be inherited from by other classes intended to act as Singleton Canvases
/// </summary>
/// <typeparam name="T">The Type of class to be a Canvas</typeparam>
public abstract class Canvas<T> : Singleton<T> where T : Singleton<T>
{
    /// <summary> CanvasGroup of the entire canvas </summary>
    [SerializeField] protected CanvasGroup canvasGroup = null;
}