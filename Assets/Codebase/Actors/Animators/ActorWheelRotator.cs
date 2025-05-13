using System;
using UnityEngine;

/// <summary>
/// Rotates a wheel body part based on the current move speed of the actor
/// </summary>
public class ActorWheelRotator : MonoBehaviour
{
    /// <summary> The base speed at which the wheel should rotate </summary>
    [SerializeField] private float rotateSpeed = 1f;
    /// <summary> The Actor to use when calculating the rotate speed </summary>
    private Actor actor;
    
    /// <summary>
    /// Start callback
    /// </summary>
    private void Start()
    {
        actor = GetComponentInParent<Actor>();
    }

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        float rotation = actor.CurrentSpeed * rotateSpeed * Time.deltaTime;
        transform.localEulerAngles += new Vector3(0, 0, rotation);
    }
}
