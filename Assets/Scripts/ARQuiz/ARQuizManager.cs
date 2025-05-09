using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization; // Add this namespace
using UnityEngine.Localization.Settings; // Add this namespace

public class ARQuizManager : MonoBehaviour
{
    public List<ARQuizQuestionSO> questions; // Assign ARQuizQuestionSO instances in Inspector
    public TMP_Text questionText; // Reference to the UI Text component for the question
    public TMP_Text progressText; // Reference to the UI Text component for progress

    private int currentQuestionIndex = 0;
    private int progress = 0;

    //private const string CorrectFeedback = "Great! You got the right answer!";
    //private const string IncorrectFeedback = "Sorry! You scanned the wrong answer. Try again!";
    public LocalizedString correctFeedback;
    public LocalizedString incorrectFeedback;
    public LocalizedString completionMessage;


    void Start()
    {
        if (questions.Count == 0)
        {
            Debug.LogError("No questions assigned to the QuizManager.");
            return;
        }

        ShuffleQuestions(); // Shuffle the question list
        UpdateQuestion();
        UpdateProgress();
    }

    private void UpdateQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            ARQuizQuestionSO currentQuestion = questions[currentQuestionIndex];
            questionText.color = Color.yellow;
            //questionText.text = currentQuestion.questionText;
            questionText.text = currentQuestion.localizedQuestionText.GetLocalizedString(); // Use localized text
        }
        else
        {
            questionText.color = Color.green;
            //questionText.text = "Well done! You have completed all questions.";
            questionText.text = completionMessage.GetLocalizedString(); // Use localized text
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
            //questionText.text = CorrectFeedback;
            questionText.text = correctFeedback.GetLocalizedString(); // Use localized text
        }
        else
        {
            questionText.color = Color.red;
            //questionText.text = IncorrectFeedback;
            questionText.text = incorrectFeedback.GetLocalizedString(); // Use localized text
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

    private void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            ARQuizQuestionSO temp = questions[i];
            int randomIndex = Random.Range(i, questions.Count);
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }
}
