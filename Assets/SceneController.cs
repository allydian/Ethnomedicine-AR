using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void ARScannerReturnToMainScene()
    {
        // Call the ToMainScene method in the ChangeScenes script
        ChangeScenes changeScenes = FindObjectOfType<ChangeScenes>();  // Find the ChangeScenes instance
        if (changeScenes != null)
        {
            // Set the currentPanel to Home before changing the scene
            AppPanelManager.instance.currentPanel = AppPanelManager.PanelType.Home;
            changeScenes.ToMainScene(AppPanelManager.PanelType.Home); // Ensure the Home panel is active
        }
        else
        {
            Debug.LogError("ChangeScenes script not found.");
        }
    }

    public void VFReturnToMainScene()
    {
        // Call the ToMainScene method in the ChangeScenes script
        ChangeScenes changeScenes = FindObjectOfType<ChangeScenes>();  // Find the ChangeScenes instance
        if (changeScenes != null)
        {
            // Set the currentPanel to Forest before changing the scene
            AppPanelManager.instance.currentPanel = AppPanelManager.PanelType.Forest;
            changeScenes.ToMainScene(AppPanelManager.PanelType.Forest); // Ensure the Forest panel is active
        }
        else
        {
            Debug.LogError("ChangeScenes script not found.");
        }
    }

    public void QuizReturnToMainScene()
    {
        // Call the ToMainScene method in the ChangeScenes script
        ChangeScenes changeScenes = FindObjectOfType<ChangeScenes>();  // Find the ChangeScenes instance
        if (changeScenes != null)
        {
            // Set the currentPanel to Quiz before changing the scene
            AppPanelManager.instance.currentPanel = AppPanelManager.PanelType.Quiz;
            changeScenes.ToMainScene(AppPanelManager.PanelType.Quiz); // Ensure the Quiz panel is active
        }
        else
        {
            Debug.LogError("ChangeScenes script not found.");
        }
    }
}
