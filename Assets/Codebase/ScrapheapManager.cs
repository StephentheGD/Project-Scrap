using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all high level functions for the Scrapheap
/// </summary>
public class ScrapheapManager : MonoBehaviour
{
    /// <summary> Prefab used to create an enemy </summary>
    [SerializeField] private GameObject enemyPrefab = null;

    /// <summary> List of all instantiated enemies </summary>
    private List<GameObject> instantiatedEnemies = new();
    
    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Setup();
    }

    /// <summary>
    /// Sets up the scrapheap
    /// Only to be called upon resetting the scrapheap
    /// </summary>
    public void Setup()
    {
        // Set up enemies
        enemyPrefab.SetActive(false);
        int enemyCount = UnityEngine.Random.Range(5, 8);
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 position = UnityEngine.Random.insideUnitSphere * 10f;
            position.y = 0f;
            
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = position;
            newEnemy.SetActive(true);
            instantiatedEnemies.Add(newEnemy);
        }
    }
}
