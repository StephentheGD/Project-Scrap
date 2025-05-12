using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Canvas for Healthbars
/// </summary>
public class HealthbarCanvas : Canvas<HealthbarCanvas>
{
    [SerializeField] private Healthbar healthbarPrefab;
    [SerializeField] private Transform healthbarContainer;

    private Dictionary<DamageHandler, Healthbar> registeredHealthbars = new();

    private void Awake()
    {
        Instance = this;
        
        healthbarPrefab.gameObject.SetActive(false);
    }

    public void RegisterHealthbar(DamageHandler damageHandler)
    {
        if (registeredHealthbars.ContainsKey(damageHandler))
            return;
        
        Healthbar healthbar = Instantiate(healthbarPrefab, healthbarContainer);
        healthbar.gameObject.SetActive(true);
        healthbar.Setup(damageHandler);
        registeredHealthbars.Add(damageHandler, healthbar);
    }

    public void DeregisterHealthbar(DamageHandler damageHandler)
    {
        if (!registeredHealthbars.ContainsKey(damageHandler))
            return;
        
        Healthbar healthbar = registeredHealthbars[damageHandler];
        Destroy(healthbar.gameObject);
        registeredHealthbars.Remove(damageHandler);
    }
}
