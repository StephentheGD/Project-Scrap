using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;

    public void Use()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
