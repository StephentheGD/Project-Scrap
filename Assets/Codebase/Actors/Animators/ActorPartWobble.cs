using UnityEngine;

/// <summary>
/// Rotates a body part based on the specified amount and speed
/// </summary>
public class ActorPartWobble : MonoBehaviour
{
    /// <summary> Amount of wobble the object should perform, measured in Sin </summary>
    [SerializeField] private float wobbleAmount = 1f;
    /// <summary> Speed at which the wobble should perform </summary>
    [SerializeField] private float wobbleSpeed = 1f;

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        float sin = Mathf.Sin(Time.time * wobbleSpeed);
        float rotation = sin * wobbleAmount;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}
