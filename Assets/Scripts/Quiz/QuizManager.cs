using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;


public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private QuizDataSO quizData;
    [SerializeField] private float timeLimit = 30f;
    private List<Question> questions;
    private Question selectedQuestion = new Question();
    private int scoreCount = 0;
    private float currentTime;
    private int livesRemaining = 3;

    private QuizDataSO quizDataSO;

    private GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus { get { return gameStatus;}}
    public QuizDataSO QuizData {get=> quizData;}

    public void Start()
    {
        scoreCount = 0;
        currentTime = timeLimit;
        livesRemaining = 3;
        questions = new List<Question>();
        quizDataSO = quizData;

        if (quizDataSO == null)
        {
            Debug.LogError("QuizDataSO is not assigned!");
            return;
        }

        questions.AddRange(quizDataSO.questions);

        if (questions.Count == 0)
        {
            Debug.LogError("No questions found in QuizDataSO!");
            return;
        }

        SelectQuestion();
        gameStatus = GameStatus.Playing;
    }
    void SelectQuestion()
    {
        int questionIndex = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[questionIndex];

        quizUI.SetQuestion(selectedQuestion);

        //Shows question only once
        questions.RemoveAt(questionIndex);
    }
    private void Update()
    {
        if (gameStatus == GameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time:" + time.ToString("mm' : 'ss");

        if (currentTime <= 0)
        {
            GameEnd();
        }
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;

        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            scoreCount += (int)Time.time;
            quizUI.ScoreText.text = "Score:" + scoreCount;
        }
        else
        {
            livesRemaining--;
            quizUI.ReduceLives(livesRemaining);

            if (livesRemaining == 0)
            {
                GameEnd();
            }
        }

        if (gameStatus == GameStatus.Playing)
        {
            if (questions.Count > 0)
            {
                Invoke("SelectQuestion",0.4f);
            }
            else
            {
                GameEnd();
            }            
        }
        return correctAns;
    }

    private void GameEnd()
    {
        gameStatus = GameStatus.Next;
        quizUI.GameOverPanel.SetActive(true);
    }
}


[System.Serializable]
public enum QuestionType
{
    text,
    image,
}

[System.Serializable]
public enum GameStatus
{
    Next,
    Playing,
}

