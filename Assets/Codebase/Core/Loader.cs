using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the loading and unloading of scenes
/// </summary>
public class Loader : Singleton<Loader>
{
    /// <summary> List of all the names of current loaded scenes </summary>
    public List<string> LoadedScenes = new();

    /// <summary> Public accessor for isBusy </summary>
    public bool IsBusy => isBusy;
    /// <summary> Whether or not the loader is current performing an operation </summary>
    private bool isBusy = false;

    /// <summary> Public accessor for isInitialised </summary>
    public bool IsInitialised => isInitialised;
    /// <summary> Whether or not the loader has been initialised </summary>
    private bool isInitialised = false;

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
        isInitialised = true;
    }

    /// <summary>
    /// Loads the Loader scene
    /// Designed to be used when there is no Loader instance
    /// </summary>
    public static async void BootstrapLoad()
    {
        if (Instance != null)
            return;
        
        // Check if the loader needs to be loaded
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Loader")
            return;
        
        SceneManager.LoadSceneAsync("Loader", LoadSceneMode.Additive);

        // Wait for the Loader component in the Loader scene to call Awake
        while (Instance == null)
            await UniTask.NextFrame();
        
        // Ensure the already existing scene is registered with the new Loader instance
        Instance.LoadedScenes.Add(currentScene.name);
    }

    /// <summary>
    /// Asynchronously loads the new scene, and unloads the old scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void SwitchScene(string sceneName)
    {
        isBusy = true;
        
        // First load scene
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        LoadedScenes.Add(sceneName);
        
        // Then unload other scenes
        List<string> unloadedScenes = new();
        foreach (var scene in LoadedScenes)
        {
            if (scene == sceneName)
                continue;
            if (scene.Contains("Loader"))
                continue;
            if (string.IsNullOrEmpty(scene))
                continue;

            SceneManager.UnloadSceneAsync(scene);
            unloadedScenes.Add(scene);
        }

        foreach (var scene in unloadedScenes)
            LoadedScenes.Remove(scene);

        isBusy = false;
    }
}
