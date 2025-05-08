using DG.Tweening;
using UnityEngine;

/// <summary>
/// UI Canvas for Pausing
/// </summary>
public class PauseCanvas : OpenableCanvas<PauseCanvas>, IOpenable
{
    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start callback
    /// </summary>
    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    #region Buttons
    /// <summary>
    /// Called when the Resume button is pressed
    /// </summary>
    public void ResumeButton()
    {
        Resume();
    }

    /// <summary>
    /// Called when the To Main Menu button is pressed
    /// </summary>
    public void ToMainMenuButton()
    {
        Loader.Instance.SwitchScene("Main Menu");
        Close();
    }
    #endregion

    /// <summary>
    /// Closes the Main Menu and resumes the game
    /// </summary>
    public void Resume()
    {
        Close();
    }

    /// <summary>
    /// Update callback
    /// </summary>
    private void Update()
    {
        if (Loader.Instance.LoadedScenes.Contains("Main Menu"))
            return;
        
        // TODO: Move this to an InputManager
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsOpen)
                Resume();
            else
                Open();
        }
    }
}
