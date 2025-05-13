using System;
using UnityEngine;

/// <summary>
/// Actor representing an enemy
/// </summary>
public class EnemyActor : Actor
{
    /// <summary> TEST: Offset for the test movement </summary>
    private float TEST_MOVEMENT_OFFSET = 0f;

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        TEST_MOVEMENT_OFFSET = UnityEngine.Random.value * 10f;
    }

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        movementVector.x = Mathf.Sin(Time.time + TEST_MOVEMENT_OFFSET);
        movementVector.z = Mathf.Cos(Time.time + TEST_MOVEMENT_OFFSET);
        
        base.Update();
    }
}
