using UnityEngine;

/// <summary>
/// Singleton class to be inherited from by other classes intended to act as Singletons
/// </summary>
/// <typeparam name="T">The Type of class to be a Singleton</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary> Singleton instance of the object of type T </summary>
    public static T Instance = null;
}
