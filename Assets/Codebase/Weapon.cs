using UnityEngine;

/// <summary>
/// Weapon object that can fire projectiles
/// TODO: Differentiate between melee and ranged weapons
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary> Prefab to use when instantiating projectiles for this weapon </summary>
    [SerializeField] private Projectile projectilePrefab;

    /// <summary>
    /// Triggers use of the weapon, creating a projectile
    /// </summary>
    public void Use()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Triggers the use of the weapon, creating a projectile in the direction specified
    /// </summary>
    /// <param name="direction">Normalised direction for the projectile to use</param>
    public void Use(Vector3 direction)
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Setup(direction);
    }
}
