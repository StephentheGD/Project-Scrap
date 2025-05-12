using System;
using UnityEngine;

/// <summary>
/// Handles health and damage for an object
/// </summary>
public class DamageHandler : MonoBehaviour
{
    public float NormalisedCurrentHealth => currentHealth / (float)maxHealth;
    
    /// <summary> Base health value for this object </summary>
    [SerializeField] private int baseHealth = 10;

    private int maxHealth = 1;
    private int currentHealth = 1;
    
    
    /// <summary> Delegate to be used when the health value changes, returning the new amount </summary>
    public delegate void HealthChangedEvent(int deltaHealth);
    /// <summary> Callback for when the health value changes </summary>
    public event HealthChangedEvent OnHealthChanged;

    private void Awake()
    {
        maxHealth = baseHealth;
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
