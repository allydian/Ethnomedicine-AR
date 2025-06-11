using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InitialisePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform quizDonePanel = this.GetComponent<RectTransform>();
        float screenHeight = Screen.height;
        quizDonePanel.anchoredPosition = new Vector2(quizDonePanel.anchoredPosition.x, -screenHeight);
        Debug.Log("Game over panel moved " + screenHeight + ".");
    }
}
