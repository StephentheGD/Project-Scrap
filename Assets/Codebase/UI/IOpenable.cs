using UnityEngine;

public interface IOpenable
{
    public bool IsOpen { get; set; }
    public bool IsTransitioning { get; set; }
    
    /// <summary>
    /// Opens the canvas
    /// </summary>
    public void Open();
    
    /// <summary>
    /// Closes the canvas
    /// </summary>
    public void Close();
}
