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
    // Loads the main scene with panels (MainPanels scene).
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainPanels");
    }

    // Loads the AR camera scene.
    public void ToARCamera()
    {
        SceneManager.LoadScene("ARcamera");
    }

    // Loads the AR quiz scene.
    public void ToARQuiz()
    {
        SceneManager.LoadScene("ARquiz");
    }

    // Loads the first viewpoint of Bako National Park.
    public void ToVFBakoNationalParkI()
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

    // Returns to the previous scene. (To be implemented)
    public void ToPrevScene()
    {
        
    }
}
