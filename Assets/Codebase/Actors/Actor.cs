using System;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Base class for all Actors
/// </summary>
public abstract class Actor : MonoBehaviour
{
    /// <summary> Public accessor for currentSpeed </summary>
    public float CurrentSpeed => currentSpeed;
    /// <summary> The current speed the actor is moving as a measure of the magnitude of the movement velocity </summary>
    protected float currentSpeed = 0;
    
    /// <summary> Container acting as a parent for all visual objects on the actor </summary>
    [Header("Actor")]
    [SerializeField] protected GameObject visuals = null;
    
    /// <summary> Speed at which the actor should move </summary>
    [SerializeField] protected float moveSpeed = 3f;
    /// <summary> Speed at which the actor should move horizontally </summary>
    protected float horizontalMoveSpeed => moveSpeed;
    /// <summary> Speed at which the actor should move vertically </summary>
    protected float verticalMoveSpeed => moveSpeed * Utils.DEPTH_SCALE;

    /// <summary> The Actor's currently equipped weapon </summary>
    [SerializeField] protected Weapon equippedWeapon = null;
    
    /// <summary> Whether or not the actor is currently facing right </summary>
    protected bool isFacingRight = false;

    public Vector3 MovementVector => movementVector;
    protected Vector3 movementVector = Vector3.zero;

    protected void Update()
    {
        // Actually move the actor
        Vector3 movement = new(movementVector.x * horizontalMoveSpeed, 0, movementVector.z * verticalMoveSpeed);
        transform.position += movement * Time.deltaTime;
        
        // Update the current speed value
        currentSpeed = new Vector2(movement.x / horizontalMoveSpeed, movement.z / verticalMoveSpeed).magnitude;
        
        // Handle facing movement
        if (movementVector.x > 0 && !isFacingRight)
        {
            visuals.transform.DOKill();
            visuals.transform.DORotate(new Vector3(0, 180, 0), 0.25f, RotateMode.Fast);
            isFacingRight = true;
        }
        else if (movementVector.x < 0 & isFacingRight)
        {
            visuals.transform.DOKill();
            visuals.transform.DORotate(new Vector3(0, 0, 0), 0.25f, RotateMode.Fast);
            isFacingRight = false;
        }
        
        // Reset movementVector
        movementVector = Vector3.zero;
    }

    /// <summary>
    /// Uses the equipped weapon if one is available
    /// </summary>
    protected void UseWeapon()
    {
        if (!equippedWeapon)
            return;
        
        // Get weapon attack direction
        Vector3 direction = equippedWeapon.transform.position - transform.position;
        direction.y = 0f;
        direction = direction.normalized;
        
        // Actually use weapon
        equippedWeapon.Use(direction);
    }
}
