using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ARQuizManager : MonoBehaviour
{
    public List<ARQuizQuestionSO> questions; // Assign ARQuizQuestionSO instances in Inspector
    public TMP_Text questionText; // Reference to the UI Text component for the question
    public TMP_Text progressText; // Reference to the UI Text component for progress

    private int currentQuestionIndex = 0;
    private int progress = 0;

    private const string CorrectFeedback = "Great! You got the right answer!";
    private const string IncorrectFeedback = "Sorry! You scanned the wrong answer. Try again!";

    void Start()
    {
        if (questions.Count == 0)
        {
            Debug.LogError("No questions assigned to the QuizManager.");
            return;
        }

        UpdateQuestion();
        UpdateProgress();
    }

    private void UpdateQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            ARQuizQuestionSO currentQuestion = questions[currentQuestionIndex];
            questionText.color = Color.yellow;
            questionText.text = currentQuestion.questionText;
        }
        else
        {
            questionText.color = Color.green;
            questionText.text = "Well done! You have completed all questions.";
        }
    }

    public void GetScannedMarker(int id)
    {
        if (currentQuestionIndex < questions.Count)
        {
            ARQuizQuestionSO currentQuestion = questions[currentQuestionIndex];

            if (id == currentQuestion.answerType)
            {
                StartCoroutine(DisplayAnswerFeedback(true));
            }
            else
            {
                StartCoroutine(DisplayAnswerFeedback(false));
            }
        }
    }

    private IEnumerator DisplayAnswerFeedback(bool isCorrect)
    {
        if (isCorrect)
        {
            questionText.color = Color.green;
            questionText.text = CorrectFeedback;
        }
        else
        {
            questionText.color = Color.red;
            questionText.text = IncorrectFeedback;
        }

        yield return new WaitForSeconds(2.0f);

        if (isCorrect)
        {
            currentQuestionIndex++;
            progress++;
            UpdateQuestion();
            UpdateProgress();
        }
        else
        {
            UpdateQuestion();
        }
    }

    private void UpdateProgress()
    {
        progressText.text = $"{progress}/{questions.Count}";
    }
}
