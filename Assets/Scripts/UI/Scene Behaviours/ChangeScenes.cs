using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

/// <summary>
/// A class responsible for changing scenes within the application.
/// Provides methods to switch to various specific scenes, such as the main menu, AR scenes, and different viewpoints of Bako National Park.
/// </summary>
public class ChangeScenes : MonoBehaviour
{
    public TransitionSettings transitionSettings; // Assign this in inspector
    public float transitionDelay = 0f;

    // Loads the main scene with panels (MainPanels scene) based on the previously opened scene.
    public void ToMainScene(AppPanelManager.PanelType panelType)
    {
        // Set the desired panel in the PanelManager
        AppPanelManager.instance.currentPanel = panelType;

        // Now load the main scene
        //SceneManager.LoadScene("MainPanels (Test) 1");
        TransitionManager.Instance().Transition("MainPanels (Test) 1", transitionSettings, transitionDelay);
    }
    public void ToMainPanels()
    {
        //SceneManager.LoadScene("MainPanels (Test) 1");
        TransitionManager.Instance().Transition("MainPanels (Test) 1", transitionSettings, transitionDelay);
    }

    // Loads the AR camera scene.
    public void ToARCamera()
    {
        //SceneManager.LoadScene("ARcamera");
        TransitionManager.Instance().Transition("ARcamera", transitionSettings, transitionDelay);
    }

    // Loads the scene for the Medicinal Plants Quiz.
    public void OpenMedicinalQuiz()
    {
        //SceneManager.LoadScene("MedicinalPlantsQuiz");
        TransitionManager.Instance().Transition("MedicinalPlantsQuiz", transitionSettings, transitionDelay);
    }

    // Loads the AR quiz scene.
    public void ToARQuiz()
    {
        //SceneManager.LoadScene("ARquiz");
        TransitionManager.Instance().Transition("ARquiz", transitionSettings, transitionDelay);
    }

    /// <summary>
    /// List of virtual forests 
    /// </summary>
    // Loads the first viewpoint of Bako National Park.

    public void ToBakoForest1()
    {
        //SceneManager.LoadScene("BakoForest 1");
        TransitionManager.Instance().Transition("BakoForest 1", transitionSettings, transitionDelay);
    }

}
