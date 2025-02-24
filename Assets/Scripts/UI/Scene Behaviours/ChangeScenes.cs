using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A class responsible for changing scenes within the application.
/// Provides methods to switch to various specific scenes, such as the main menu, AR scenes, and different viewpoints of Bako National Park.
/// </summary>
public class ChangeScenes : MonoBehaviour
{
    // Loads the main scene with panels (MainPanels scene) based on the previously opened scene.
    public void ToMainScene(AppPanelManager.PanelType panelType)
    {
        // Set the desired panel in the PanelManager
        AppPanelManager.instance.currentPanel = panelType;
        
        // Now load the main scene
        SceneManager.LoadScene("MainPanels (Test)");
    }
    public void ToMainPanels()
    {
        SceneManager.LoadScene("MainPanels (Test)");
    }

    // Loads the AR camera scene.
    public void ToARCamera()
    {
        SceneManager.LoadScene("ARcamera");
    }

    // Loads the scene for the Medicinal Plants Quiz.
    public void OpenMedicinalQuiz()
    {
        SceneManager.LoadScene("MedicinalPlantsQuiz");
    }

    // Loads the AR quiz scene.
    public void ToARQuiz()
    {
        SceneManager.LoadScene("ARquiz");
    }

    /// <summary>
    /// List of virtual forests 
    /// </summary>
    // Loads the first viewpoint of Bako National Park.
    /*public void ToVFBakoNationalParkI()
    {
        SceneManager.LoadScene("Bako National Park I");
    }

    // Loads the second viewpoint of Bako National Park.
    public void ToVFBakoNationalParkII()
    {
        SceneManager.LoadScene("Bako National Park II");
    }

    // Loads the third viewpoint of Bako National Park.
    public void ToVFBakoNationalParkIII()
    {
        SceneManager.LoadScene("Bako National Park III");
    }

    // Loadsthe gyro version for all viewpoints in Bako.
    public void ToBakoGyro()
    {
        SceneManager.LoadScene("BakoGyro");
    }*/

    public void ToBakoForest()
    {
        SceneManager.LoadScene("BakoForest");
    }

    public void ToBakoForest1()
    {
        SceneManager.LoadScene("BakoForest 1");
    }

}
