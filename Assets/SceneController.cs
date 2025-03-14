using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void ARScannerReturnToMainScene()
    {
        // Call the ToMainScene method in the ChangeScenes script
        ChangeScenes changeScenes = FindObjectOfType<ChangeScenes>();  // Find the ChangeScenes instance
        if (changeScenes != null)
        {
            // Pass the PanelType.Home to ensure the Home panel is active
            changeScenes.ToMainScene(AppPanelManager.PanelType.Home);
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
            // Pass the PanelType.Quiz to ensure the VF list panel is active
            changeScenes.ToMainScene(AppPanelManager.PanelType.Forest);
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
            // Pass the PanelType.Quiz to ensure the Quiz list panel is active
            changeScenes.ToMainScene(AppPanelManager.PanelType.Quiz);
        }
        else
        {
            Debug.LogError("ChangeScenes script not found.");
        }
    }
}