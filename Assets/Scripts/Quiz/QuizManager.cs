using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;
using TMPro;

/// <summary>
/// Manages the overall quiz logic, including question selection, timing, score calculation,
/// and handling of game states such as game over.
/// </summary>
public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;  // Reference to the UI manager for quiz display.
    [SerializeField] private QuizDataSO quizData;  // Reference to ScriptableObject holding quiz data.
    [SerializeField] private float timeLimit = 30f;  // Time limit for each quiz round.

    private List<Question> questions;  // List of questions available for the quiz.
    private Question selectedQuestion = new Question();  // The current question being displayed.
    private QuizDataSO quizDataSO;  // A local reference to the quiz data.

    private int scoreCount = 0;  // Tracks the user's score.
    private int livesRemaining = 3;  // Tracks remaining lives for the player.
    private int correctAnswerCount = 0;  // Tracks number of correct answers.
    private float currentTime;  // The current time left for the quiz round.

    public TMP_Text finalScoreText;  // Reference to the final score text in the game over panel.
    public TMP_Text correctAnswersText;  // Reference to the correct answers text in the game over panel.

    private GameStatus gameStatus = GameStatus.Next;  // Tracks the current game state (Playing or Next).
    private CheckAchievements checkAchievements;  // Reference to CheckAchievements
    
    // Property to expose the game status.
    public GameStatus GameStatus { get { return gameStatus;} }
    
    // Property to expose the quiz data.
    public QuizDataSO QuizData { get => quizData; }

    /// <summary>
    /// Initializes the quiz at the start of the game. Loads the questions and sets up initial game state.
    /// </summary>
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

        // Initialize the CheckAchievements script
        checkAchievements = FindObjectOfType<CheckAchievements>();
        if (checkAchievements == null)
        {
            Debug.LogError("CheckAchievements script not found!");
        }

        SelectQuestion();
        gameStatus = GameStatus.Playing;
    }

    /// <summary>
    /// Randomly selects a question from the remaining questions and removes it from the list.
    /// Passes the selected question to the UI for display.
    /// </summary>
    void SelectQuestion()
    {
        currentTime = timeLimit; // Resets the time limit each time a new question is loaded
        int questionIndex = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[questionIndex];

        quizUI.SetQuestion(selectedQuestion);

        //Shows question only once
        questions.RemoveAt(questionIndex); // Ensures the same question does not show again.
    }

    /// <summary>
    /// Updates the quiz timer and checks for game over if time runs out.
    /// </summary>
    private void Update()
    {
        if (gameStatus == GameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    /// <summary>
    /// Sets the quiz timer display in the UI and ends the game if the time runs out.
    /// </summary>
    /// <param name="value">The current time value to display.</param>
    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time:" + time.ToString("ss' : 'fff");

        if (currentTime <= 0)
        {
            GameEnd();
        }
    }

    /// <summary>
    /// Handles the logic when an answer is submitted, updates score and checks for correct answer.
    /// </summary>
    /// <param name="answered">The string representation of the selected answer.</param>
    /// <returns>True if the answer is correct, false otherwise.</returns>
    public bool Answer(string answered)
    {
        bool correctAns = false;

        int baseScoreMultiplier = 10; // The base multiplier for score calculation.

        // Check if the selected answer is correct.
        if(answered == selectedQuestion.options[selectedQuestion.correctAns])
        {
            correctAns = true;

            correctAnswerCount++;

            // Convert remaining time to milliseconds
            float currentTimeInMilliseconds = currentTime * 1000f;  // Convert to milliseconds
            float totalTimeInMilliseconds = 30f * 1000f;  // Assuming total time is 30 seconds, convert to milliseconds

            // Calculate time multiplier based on milliseconds
            float timeMultiplier = currentTimeInMilliseconds / totalTimeInMilliseconds;


            // Apply time multiplier to the score increment
            int scoreIncrement = Mathf.CeilToInt(baseScoreMultiplier * timeMultiplier);  // Rounds up to nearest integer
            scoreCount += scoreIncrement;

            quizUI.ScoreText.text = "Score:" + scoreCount;
        }
        else
        {
            livesRemaining--; // Reduce remaining lives if the answer is wrong.
            quizUI.ReduceLives(livesRemaining);

            if (livesRemaining == 0)
            {
                Invoke("GameEnd", 1f); // Delay game end by 1 second.
            }
        }

        // Continue the quiz if there are more questions, otherwise end the game.
        if (gameStatus == GameStatus.Playing)
        {
            if (questions.Count > 0)
            {
                Invoke("SelectQuestion", 0.4f); // Short delay before next question.
            }
            else
            {
                Invoke("GameEnd", 1f); // Delay game end if all questions are answered.
            }            
        }
        return correctAns;
    }

    /// <summary>
    /// Ends the quiz, displays the game over panel, and shows the final score and correct answers count.
    /// </summary>
    private void GameEnd()
    {
        gameStatus = GameStatus.Next; // Set game state to Next
        quizUI.GameOverPanel.SetActive(true); // Show the game over panel.

        finalScoreText.text = "Total score: " + scoreCount; // Display the total score in the final score text field.

        correctAnswersText.text = "Correct answers: " + correctAnswerCount + "/ N"; // Display the number of correct answers.

        // Check for achievements at the end of the game
        //CheckAchievements();
        // Call the achievement check methods in CheckAchievements
        if (checkAchievements != null)
        {
            checkAchievements.CheckSurvivalist(livesRemaining);  // Check for the Survivalist achievement
            checkAchievements.CheckTimeKeeper(currentTime, livesRemaining);  // Check for the Time Keeper achievement
        }

        quizUI.gameObject.SetActive(false); // Optionally, hide the quiz UI.
    }

     /// <summary>
    /// Checks if any achievements should be unlocked based on the game result.
    /// </summary>
    private void CheckAchievements()
    {
        // Check for the "Survivalist" achievement (complete without losing lives)
        if (livesRemaining == 3)
        {
            AchievementManager.instance.Unlock("Survivalist");
            Debug.Log("Survivalist achievement unlocked!");
        }

        // Check for the "Time Keeper" achievement (complete within time limit with at least one life left)
        if (currentTime > 0f && livesRemaining >= 1)
        {
            AchievementManager.instance.Unlock("TimeKeeper");
            Debug.Log("Time Keeper achievement unlocked!");
        }
    }
}

// The QuestionType enum defines the different types of questions 
// that the quiz can support (text-based or image-based questions).
[System.Serializable]
public enum QuestionType
{
    text,   // A question that is purely textual.
    image   // A question that displays an image.
}

// The GameStatus enum is used to track the current state of the quiz game. 
// This helps manage transitions between playing the quiz and the next state 
[System.Serializable]
public enum GameStatus
{
    Next, // Represents a state where the quiz is ready to move to the next question or screen.
    Playing     // Represents a state where the quiz is actively running, and the player is answering questions.
}

