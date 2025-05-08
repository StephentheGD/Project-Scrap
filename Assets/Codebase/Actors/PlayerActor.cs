using DG.Tweening;
using UnityEngine;

/// <summary>
/// Actor controlled by the player
/// </summary>
public class PlayerActor : MonoBehaviour
{
    /// <summary> Public accessor for currentSpeed </summary>
    public float CurrentSpeed => currentSpeed;
    /// <summary> The current speed the actor is moving as a measure of the magnitude of the movement velocity </summary>
    private float currentSpeed = 0;
    
    /// <summary> Container acting as a parent for all visual objects on the actor </summary>
    [SerializeField] private GameObject visuals = null;

    /// <summary> Speed at which the actor should move horizontally </summary>
    [SerializeField] private float horizontalMoveSpeed = 1f;
    /// <summary> Speed at which the actor should move vertically </summary>
    [SerializeField] private float verticalMoveSpeed = 1f;
    /// <summary> Maximum distance the player should be able to perform an interaction </summary>
    [SerializeField] private float maxInteractionDistance = 1f;
    
    /// <summary> Whether or not the actor is currently facing right </summary>
    private bool isFacingRight = false;
    
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
        Vector3 movement = new();
        
        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left * (Time.deltaTime * horizontalMoveSpeed);
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right * (Time.deltaTime * horizontalMoveSpeed);
        if (Input.GetKey(KeyCode.W))
            movement += Vector3.forward * (Time.deltaTime * verticalMoveSpeed);
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.back * (Time.deltaTime * verticalMoveSpeed);

        transform.position += movement;
        currentSpeed = new Vector2(movement.x / horizontalMoveSpeed, movement.z / verticalMoveSpeed).magnitude;
        
        // Handle facing movement
        if (movement.x > 0 && !isFacingRight)
        {
            visuals.transform.DOKill();
            visuals.transform.DORotate(new Vector3(0, 180, 0), 0.25f, RotateMode.Fast);
            isFacingRight = true;
        }
        else if (movement.x < 0 & isFacingRight)
        {
            visuals.transform.DOKill();
            visuals.transform.DORotate(new Vector3(0, 0, 0), 0.25f, RotateMode.Fast);
            isFacingRight = false;
        }
        
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
    }
}
