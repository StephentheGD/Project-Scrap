using System;
using UnityEngine;

/// <summary>
/// Actor representing an enemy
/// </summary>
public class EnemyActor : Actor
{
    /// <summary> TEST: Offset for the test movement </summary>
    private float TEST_MOVEMENT_OFFSET = 0f;
    /// <summary> TEST: Cooldown timer for weapon usage </summary>
    private float TEST_WEAPON_COOLDOWN = 0f;

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        TEST_MOVEMENT_OFFSET = UnityEngine.Random.value * 10f;
        TEST_WEAPON_COOLDOWN = UnityEngine.Random.Range(2f, 3f);
    }

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        // TEST: Move in a circle automatically
        movementVector.x = Mathf.Sin(Time.time + TEST_MOVEMENT_OFFSET);
        movementVector.z = Mathf.Cos(Time.time + TEST_MOVEMENT_OFFSET);

        // TEST: Use weapon every few seconds
        if (TEST_WEAPON_COOLDOWN <= 0)
        {
            UseWeapon();
            TEST_WEAPON_COOLDOWN += UnityEngine.Random.Range(2f, 3f);
        }
        TEST_WEAPON_COOLDOWN -= Time.deltaTime;
        
        base.Update();
    }
}
