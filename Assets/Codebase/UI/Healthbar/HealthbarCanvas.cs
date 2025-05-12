using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Canvas for Healthbars
/// </summary>
public class HealthbarCanvas : Canvas<HealthbarCanvas>
{
    /// <summary> Prefab used to instantiate a new healthbar </summary>
    [SerializeField] private Healthbar healthbarPrefab;
    /// <summary> Container for all instantiated healthbars </summary>
    [SerializeField] private Transform healthbarContainer;

    /// <summary> Dictionary storing all instantiated healthbars with their registered DamageHandlers </summary>
    private Dictionary<DamageHandler, Healthbar> registeredHealthbars = new();

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Instance = this;
        
        healthbarPrefab.gameObject.SetActive(false);
    }

    /// <summary>
    /// Instantiates a Healthbar, and registers the DamageHandler to it
    /// </summary>
    /// <param name="damageHandler">The DamageHandler to instantiate a Healthbar for</param>
    public void RegisterHealthbar(DamageHandler damageHandler)
    {
        if (registeredHealthbars.ContainsKey(damageHandler))
            return;
        
        Healthbar healthbar = Instantiate(healthbarPrefab, healthbarContainer);
        healthbar.gameObject.SetActive(true);
        healthbar.Setup(damageHandler);
        registeredHealthbars.Add(damageHandler, healthbar);
    }

    /// <summary>
    /// Deregisters the DamageHandler from the Healthbar, and destroys the Healthbar
    /// </summary>
    /// <param name="damageHandler">The DamageHandler to deregister the Healthbar from</param>
    public void DeregisterHealthbar(DamageHandler damageHandler)
    {
        if (!registeredHealthbars.ContainsKey(damageHandler))
            return;
        
        Healthbar healthbar = registeredHealthbars[damageHandler];
        Destroy(healthbar.gameObject);
        registeredHealthbars.Remove(damageHandler);
    }
}
