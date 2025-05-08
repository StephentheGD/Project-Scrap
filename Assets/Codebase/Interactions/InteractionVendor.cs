using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable component that calls Vendor related functions
/// </summary>
public class InteractionVendor : MonoBehaviour, IInteractable
{
    /// <summary> Struct containing data for a single stock item </summary>
    [Serializable]
    public struct StockElement
    {
        public string Name;
        public int Price;
    }

    /// <summary> List of all StockElements assigned to this Vendor </summary>
    [SerializeField] private List<StockElement> stockElements = new();
    
    #region IInteractable
    /// <summary>
    /// Begins the interaction with the interactable component
    /// </summary>
    /// <param name="ip">The InteractionPoint that called Interact</param>
    public void Interact(InteractionPoint ip)
    {
        VendorCanvas.Instance.Open(stockElements);
    }
    #endregion
}
