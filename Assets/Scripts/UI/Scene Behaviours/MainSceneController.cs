using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject quizListPanel;
    public GameObject forestListPanel;

    void Start()
    {
        // Check the current panel from AppNavManager and activate the appropriate one
        switch (AppPanelManager.instance.currentPanel)
        {
            case AppPanelManager.PanelType.Quiz:
                quizListPanel.SetActive(true);
                homePanel.SetActive(false);
                forestListPanel.SetActive(false);
                break;

            case AppPanelManager.PanelType.Forest:
                forestListPanel.SetActive(true);
                homePanel.SetActive(false);
                quizListPanel.SetActive(false);
                break;

            default:
                homePanel.SetActive(true);
                quizListPanel.SetActive(false);
                forestListPanel.SetActive(false);
                break;
        }
    }
}