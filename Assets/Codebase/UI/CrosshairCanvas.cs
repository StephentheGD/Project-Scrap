using System;
using UnityEngine;

/// <summary>
/// UI Canvas for the Weapon Crosshair
/// </summary>
public class CrosshairCanvas : Canvas<CrosshairCanvas>
{
    /// <summary> Transform containing the crosshair image </summary>
    [SerializeField] private Transform crosshairTransform;
    
    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        crosshairTransform.position = Input.mousePosition;
    }
}
