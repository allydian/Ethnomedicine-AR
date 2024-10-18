using UnityEngine;
using UnityEngine.SceneManagement;

public class AppPanelManager : MonoBehaviour
{
    public static AppPanelManager instance;

    public enum PanelType
    {
        Home,
        Quiz,
        Forest
    }

    public PanelType currentPanel = PanelType.Home;  // Track the current panel state

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
