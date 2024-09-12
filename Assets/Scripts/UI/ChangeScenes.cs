using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainPanels");
    }
    public void ToARCamera()
    {
        SceneManager.LoadScene("ARcamera");
    }
    public void ToARQuiz()
    {
        SceneManager.LoadScene("ARquiz");
    }
}
