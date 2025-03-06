using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public LoadingScreenManager loadingScreen; // Assign in Inspector
    private string sceneToLoad;
    private bool isReloadingScene = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Loads a new scene with a transition.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        isReloadingScene = false;

        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
            loadingScreen.RevealLoadingScreen(); // Start fade-in animation
        }
        else
        {
            StartSceneLoading(); // Load directly if no loading screen
        }
    }

    /// <summary>
    /// Reloads the current active scene (used when changing locale).
    /// </summary>
    public void ReloadCurrentScene()
    {
        sceneToLoad = SceneManager.GetActiveScene().name;
        isReloadingScene = true;

        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
            loadingScreen.RevealLoadingScreen();
        }
        else
        {
            StartSceneLoading();
        }
    }

    public void StartSceneLoading()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); // Small delay for smooth transition
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        // Hide loading screen after the scene loads
        if (loadingScreen != null)
        {
            loadingScreen.HideLoadingScreen();
        }
    }
}
