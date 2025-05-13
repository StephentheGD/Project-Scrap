using System;
using UnityEngine;

/// <summary>
/// Handles an object intended to be used as a weapon's projectile.
/// Deals damage to objects it collides with.
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary> Speed at which the projectile moves </summary>
    [SerializeField] private float speed = 1f;
    /// <summary> Duration the projectile </summary>
    [SerializeField] private float lifeTime = 1000f;

    /// <summary> Normalised direction the projectile should move </summary>
    private Vector3 direction = Vector3.right;

    /// <summary>
    /// Sets up the projectile before use
    /// </summary>
    /// <param name="direction">Normalised direction the projectile should move</param>
    public void Setup(Vector3 direction)
    {  
        this.direction = direction;
    }
    
    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        // Move projectile
        transform.position += direction * (Time.deltaTime * speed);
        
        // Decay lifetime
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    /// <summary>
    /// OnTriggerEnter callback
    /// </summary>
    /// <param name="other">The collider this object has collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if eligible to be hit
        DamageHandler hit = other.GetComponent<DamageHandler>();
        if (hit == null)
            return;
        
        // Deal damage to hit object
        hit.Damage(1);
        Destroy(gameObject);
    }
}
