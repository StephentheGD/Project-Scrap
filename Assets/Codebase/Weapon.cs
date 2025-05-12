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
}
