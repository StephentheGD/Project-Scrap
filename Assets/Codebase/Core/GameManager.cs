using System;
using UnityEngine;

/// <summary>
/// Handles all high level game operations
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary> The current GameState </summary>
    public GameState GameState = null;

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start callback
    /// </summary>
    private void Start()
    {
        GameState = new GameState();
    }
}
