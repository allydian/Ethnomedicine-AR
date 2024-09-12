using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QtnHandler : MonoBehaviour
{
    private List<Questions> questionList = new List<Questions>();
    private GameObject questionText;
    private GameObject progressText;
    private Questions currentQuestion;
    private IEnumerator coroutine;
    private int progress = 0;

    private string correct = "Great! You got the right answer!";
    private string incorrect = "Sorry! You scanned the wrong answer. Try again!";

    //private Color32 turquoise = new Color32(62, 144, 180, 255);

    void Awake()
    {
        CreateQuestions();
        questionText = transform.GetChild(0).gameObject;
        progressText = transform.GetChild(1).gameObject;
    }

    void Start()
    {
        UpdateQuestion();
    }

    private void UpdateQuestion()
    {
        currentQuestion = questionList[Random.Range(0, questionList.Count - 1)];
        questionText.GetComponent<TMP_Text>().color = Color.yellow;
        questionText.GetComponent<TMP_Text>().text = currentQuestion.question;
    }

    public void GetScannedMarker(int id)
    {
        if (id == currentQuestion.ansType)
        {
            coroutine = DisplayAnswerFeedback(true);
        }
        else

        {
            coroutine = DisplayAnswerFeedback(false);
        }
        StartCoroutine(coroutine);
    }

    private IEnumerator DisplayAnswerFeedback(bool answerCorrect)
    {
        if (answerCorrect)
        {
            questionText.GetComponent<TMP_Text>().color = Color.green;
            questionText.GetComponent<TMP_Text>().text = correct;
        }
        else
        {
            questionText.GetComponent<TMP_Text>().color = Color.red;
            questionText.GetComponent<TMP_Text>().text = incorrect;
        }

        yield return new WaitForSeconds(2.0f);
        if (answerCorrect)
        {
            RemoveAnsweredQuestion();
            UpdateQuestion();
            UpdateProgress();
        }
        else
        {
            questionText.GetComponent<TMP_Text>().color = Color.yellow;
            questionText.GetComponent<TMP_Text>().text = currentQuestion.question;
        }
    }

    private void RemoveAnsweredQuestion()
    {
        int index = questionList.FindIndex(a => a.ansType == currentQuestion.ansType);
        Debug.Log("Removing Index #" + index);
        questionList.RemoveAt(index);
    }

    private void UpdateProgress()
    {
        progress++;
        if (progress < 5)
        {
            progressText.GetComponent<TMP_Text>().text = progress + "/5";
        }
        else
        {
            progressText.GetComponent<TMP_Text>().text = progress + "/5";
            questionText.GetComponent<TMP_Text>().text = "Well done! You have completed all questions.";
        }
    }

    /** Not very pretty but quick insert of the questions. 
        Should later be replaced with JSON loading or similiar. */
    private void CreateQuestions()
    {
        // 1 - ginger
        Questions qu01 = new Questions("The root of this plant is believed to aid digestion, boost the immune system, and provide relief from various ailments such as nausea and arthritis.", 1);
        questionList.Add(qu01);
        // 2 - fern
        Questions qu02 = new Questions("Boiling and consuming this plant benefits us with its anti-inflammatory properties, digestive health support, and potential to detoxify the body, particularly the liver.", 2);
        questionList.Add(qu02);
        // 3 - long
        Questions qu03 = new Questions("The roots of this plant are cut, dried, and then boiled, with the resulting water consumed as a traditional remedy within the community.", 3);
        questionList.Add(qu03);
        // 4 - tobacco
        Questions qu04 = new Questions("The leaves of this plant, when dried and crushed, are sometimes applied as a poultice to reduce swelling, relieve pain, and treat insect bites or skin irritations.", 4);
        questionList.Add(qu04);
        // 5 - pepper
        Questions qu05 = new Questions("When boiling the fruit of this plant in water, the liquid is consumed to treat digestive issues, respiratory conditions, and improve circulation, as it contains compounds that stimulate the digestive system and act as natural anti-inflammatories.", 5);
        questionList.Add(qu05);
        

        Questions qu06 = new Questions("This is a dummy question", 6);
        questionList.Add(qu06);
    }
}