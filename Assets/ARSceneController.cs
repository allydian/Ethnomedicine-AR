using UnityEngine;

public class ARSceneController : MonoBehaviour
{
    public void ReturnToMainScene()
    {
        // Call the ToMainScene method in the ChangeScenes script
        ChangeScenes changeScenes = FindObjectOfType<ChangeScenes>();  // Find the ChangeScenes instance
        if (changeScenes != null)
        {
            // Pass the PanelType.Quiz to ensure the VF list panel is active
            changeScenes.ToMainScene(AppPanelManager.PanelType.Quiz);
        }
        else
        {
            Debug.LogError("ChangeScenes script not found.");
        }
    }
}