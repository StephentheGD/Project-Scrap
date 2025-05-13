using DG.Tweening;
using UnityEngine;

/// <summary>
/// Actor controlled by the player
/// </summary>
public class PlayerActor : Actor
{
    /// <summary> Maximum distance the player should be able to perform an interaction </summary>
    [Header("Player Actor")]
    [SerializeField] private float maxInteractionDistance = 1f;
    
    /// <summary>
    /// Start callback
    /// </summary>
    private void Start()
    {
        // Load Loader from here in case it isn't initialised
        Loader.BootstrapLoad();
    }

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        // Don't do anything if currently in dialogue
        if (DialogueCanvas.Instance)
            if (DialogueCanvas.Instance.IsOpen)
                return;

        // Handle Movement
        HandleMovement();
        
        // Handle pointing weapon at cursor
        HandleAiming();
        
        // Handle Interaction Points
        HandleInteractions();

        // Handle weapon inputs
        HandleWeapon();
        
        base.Update();
    }

    /// <summary>
    /// Handles all functions related to movement
    /// </summary>
    private void HandleMovement()
    {
        // TODO: Create a PlayerInput class and receive input through the Unity Input System
        if (Input.GetKey(KeyCode.A))
            movementVector += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movementVector += Vector3.right;
        if (Input.GetKey(KeyCode.W))
            movementVector += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movementVector += Vector3.back;
    }

    /// <summary>
    /// Handles all functions related to aiming
    /// </summary>
    private void HandleAiming()
    {
        if (!equippedWeapon) 
            return;
        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(mouseRay, out RaycastHit hit, 100f, LayerMask.GetMask("Aim"))) 
            return;
        
        Debug.DrawRay(hit.point, Vector3.up, Color.green);
        Vector3 direction = hit.point - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        
        // Zero the y-axis, then normalise, THEN apply original y to correctly place on ring around player
        direction.y = 0f;
        direction = direction.normalized;
        direction.y = equippedWeapon.transform.position.y;
            
        equippedWeapon.transform.localPosition = direction;
        Debug.DrawRay(equippedWeapon.transform.position, direction, Color.blue);
    }
    
    /// <summary>
    /// Handles all functions related to interaction
    /// </summary>
    private void HandleInteractions()
    {
        float shortestDistance = Mathf.Infinity;
        InteractionPoint nearestInteractionPoint = null;
        foreach (var point in InteractionPoint.Instances)
        {
            float magnitude = (point.transform.position - transform.position).magnitude;
            if (magnitude < maxInteractionDistance)
            {
                point.DisplayPrompt();
                if (!(magnitude < shortestDistance))
                    continue;
                
                shortestDistance = magnitude;
                nearestInteractionPoint = point;
            }
            else
                point.HidePrompt();
        }

        // Handle interact inputs
        if (!Input.GetKeyDown(KeyCode.E)) 
            return;
        
        if (nearestInteractionPoint != null)
            nearestInteractionPoint.Interact();
    }

    /// <summary>
    /// Handles all functions related to using weapons
    /// </summary>
    private void HandleWeapon()
    {
        if (Input.GetMouseButtonDown(0))
            UseWeapon();
    }
}
