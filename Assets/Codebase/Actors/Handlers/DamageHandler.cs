using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Handles health and damage for an object
/// </summary>
public class DamageHandler : MonoBehaviour
{
    /// <summary> Current health for this object normalised between 0 and maxHealth </summary>
    public float NormalisedCurrentHealth => currentHealth / (float)maxHealth;
    
    /// <summary> Base health value for this object </summary>
    [SerializeField] private int baseHealth = 10;

    /// <summary> The current maximum health for this object </summary>
    private int maxHealth = 1;
    /// <summary> The current health value for this object </summary>
    private int currentHealth = 1;
    
    
    /// <summary> Delegate to be used when the health value changes, returning the new amount </summary>
    public delegate void HealthChangedEvent(int deltaHealth);
    /// <summary> Callback for when the health value changes </summary>
    public event HealthChangedEvent OnHealthChanged;

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        maxHealth = baseHealth;
        currentHealth = maxHealth;
    }

    /// <summary>
    /// OnEnable callback
    /// </summary>
    private void OnEnable()
    {
        TryRegisterHealthbar();
    }

    /// <summary>
    /// OnDisable callback
    /// </summary>
    private void OnDisable()
    {
        TryDeregisterHealthbar();
    }

    /// <summary>
    /// Attempts to register this DamageHandler with a healthbar
    /// </summary>
    private async void TryRegisterHealthbar()
    {
        while (HealthbarCanvas.Instance == null)
            await UniTask.NextFrame();
        
        HealthbarCanvas.Instance.RegisterHealthbar(this);
    }

    /// <summary>
    /// Attempts to deregister this DamageHandler with a healthbar
    /// </summary>
    private async void TryDeregisterHealthbar()
    {
        if (HealthbarCanvas.Instance == null)
            return;
        
        HealthbarCanvas.Instance.DeregisterHealthbar(this);
    }
    

    /// <summary>
    /// Deals damage to the object, and kills it if the health drops to 0 or lower
    /// </summary>
    /// <param name="damage">The change in health</param>
    public void Damage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged(damage);

        if (currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// Kills the object, destroying it
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
}
