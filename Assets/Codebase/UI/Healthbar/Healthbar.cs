using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI element displaying the health information about a single DamageHandler
/// </summary>
public class Healthbar : MonoBehaviour
{
    /// <summary> Image used to represent the health fill value </summary>
    [SerializeField] private Image fillImage;
    
    /// <summary> DamageHandler registered with this Healthbar </summary>
    private DamageHandler damageHandler;

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        if (damageHandler == null)
            return;
        
        transform.position = Camera.main.WorldToScreenPoint(damageHandler.transform.position);
    }

    /// <summary>
    /// Sets up this Healthbar, registering it with a DamageHandler
    /// </summary>
    /// <param name="damageHandler"></param>
    public void Setup(DamageHandler damageHandler)
    {
        this.damageHandler = damageHandler;
        this.damageHandler.OnHealthChanged += OnHealthChanged;
        
        SetFill(damageHandler.NormalisedCurrentHealth);
    }

    /// <summary>
    /// OnDestroy callback
    /// </summary>
    private void OnDestroy()
    {
        if (damageHandler != null)
            damageHandler.OnHealthChanged -= OnHealthChanged;
    }

    /// <summary>
    /// Manually sets the fill amount on the fillImage
    /// </summary>
    /// <param name="fillAmount">The normalised amount of fill for the fill image</param>
    public void SetFill(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
    }

    /// <summary>
    /// Called when the registered DamageHandler's currentHealth value changes
    /// </summary>
    /// <param name="deltaHealth">The change in health</param>
    private void OnHealthChanged(int deltaHealth)
    {
        SetFill(damageHandler.NormalisedCurrentHealth);
    }
}
