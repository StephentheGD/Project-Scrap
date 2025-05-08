/// <summary>
/// UI Canvas for the Main Menu
/// </summary>
public class MainMenuCanvas : Canvas<MainMenuCanvas>
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
        // Load Loader from here in case it isn't initialised
        Loader.BootstrapLoad();
    }

    /// <summary>
    /// Switches the current scene to the one with the specified name
    /// </summary>
    /// <param name="sceneName">The name of the scene to load</param>
    public void TestSceneButton(string sceneName)
    {
        Loader.Instance.SwitchScene(sceneName);
    }
}
