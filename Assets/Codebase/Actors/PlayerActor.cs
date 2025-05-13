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
        if (DialogueCanvas.Instance)
            if (DialogueCanvas.Instance.IsOpen)
                return;

        // Handle Movement
        // TODO: Create a PlayerInput class and receive input through the Unity Input System
        if (Input.GetKey(KeyCode.A))
            movementVector += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movementVector += Vector3.right;
        if (Input.GetKey(KeyCode.W))
            movementVector += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movementVector += Vector3.back;
        
        // Handle Interaction Points
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearestInteractionPoint != null)
                nearestInteractionPoint.Interact();
        }

        // Handle weapon inputs
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (equippedWeapon != null)
                equippedWeapon.Use();
        }
        
        base.Update();
    }
}
