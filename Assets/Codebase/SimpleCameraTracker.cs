using System;
using UnityEngine;

/// <summary>
/// Smoothly moves the camera to track the target
/// </summary>
public class SimpleCameraTracker : MonoBehaviour
{
    /// <summary> The target Transform to move the camera with </summary>
    [SerializeField] private Transform target = null;
    /// <summary> The maximum difference between the camera and the target on the X axis </summary>
    [SerializeField] private float targetBounds = 2f;
    /// <summary> The speed at which the camera should move to track the target </summary>
    [SerializeField] private float followSpeed = 2f;

    private void Update()
    {
        // Calculate bounds
        float upperBound = transform.position.x + targetBounds;
        float lowerBound = transform.position.x - targetBounds;
        
        // Move to keep the target within frame
        if (target.position.x > upperBound)
            transform.position += Time.deltaTime * followSpeed * Vector3.right;
        if (target.position.x < lowerBound)
            transform.position += Time.deltaTime * followSpeed * Vector3.left;
        
        // NOTE: Vectors should be at the end of the operation to reduce operation complexity
    }
}
