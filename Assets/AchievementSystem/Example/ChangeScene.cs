using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ToNextScene()
    {
        SceneManager.LoadScene("Example 2");
    }

    public void ToPrevScene()
    {
        SceneManager.LoadScene("Example 1");
    }
}
