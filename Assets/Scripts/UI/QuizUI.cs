using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;


public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager; // Reference to the QuizManager to handle quiz logic
    [SerializeField] private TMP_Text questionText, scoreText, timerText; // UI elements for displaying question, score, and timer
    [SerializeField] private List<Image> lifeImageList; // UI images representing lives in the game
    [SerializeField] private GameObject gameOverPanel; // Panel that appears when the game is over
    [SerializeField] private Image questionImage; // UI image used to display the question image if required
    [SerializeField] private List<Button> options; // Buttons for the answer options
    [SerializeField] private Color correctCol, wrongCol, lostLifeCol, normalCol; // Colors for the buttons depending on the answer status

    private Question question; // Stores the current question
    private bool answered; // Flag to track if the current question has been answered

    // Properties to access scoreText and timerText
    public TMP_Text ScoreText { get { return scoreText; } }
    public TMP_Text TimerText { get { return timerText; } }

    // Property to access the Game Over panel
    public GameObject GameOverPanel { get { return gameOverPanel; } }

    // Setup button listeners for answer options on awake
    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
                Button localBtn = options[i];
                localBtn.onClick.AddListener(()=>OnClick(localBtn));  // Adds a listener to each answer button
        }
    }

    // Sets the current question on the UI and randomizes the answer options
    public void SetQuestion(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.text:
                questionImage.transform.parent.gameObject.SetActive(false); // Hide the image holder for text questions
                break;
            case QuestionType.image:
                ImageHolder(); // Prepare the image holder for image questions
                questionImage.transform.gameObject.SetActive(true); // Show the question image
                questionImage.sprite = question.questionImg; // Set the question image
                break;
        }

        //questionText.text = question.questionInfo; // Set the question text
        questionText.text = question.questionInfo.GetLocalizedString();

        // Randomise the answer options using a shuffle method
        //List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);
         List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options.ConvertAll(option => option.GetLocalizedString()));

        // Set the text and reset colors for each answer button
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TMP_Text>().text = answerList[i]; // Set randomized answers
            options[i].name = answerList[i]; // Set button name to the answer text for identification
            options[i].image.color = normalCol; // Reset button color to normal
        }

        answered = false; // Reset answered flag for the new question
    }

    // Prepares the image holder for image-based questions
    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true); // Enable the image container
        questionImage.transform.gameObject.SetActive(false); // Initially hide the image
    }

    // Called when an answer button is clicked
    private void OnClick(Button btn)
    {
        if(quizManager.GameStatus == GameStatus.Playing)
        {  if(!answered) // Only allow one answer per question
            {
                string selectedAnswer = btn.GetComponentInChildren<TMP_Text>().text; // Get the selected answer text
                bool questionIndex = quizManager.Answer(selectedAnswer); // Check if the selected answer is correct

                if (questionIndex)
                {
                    btn.image.color = correctCol; // Change the button color to green for correct answer
                }
                else
                {
                    btn.image.color = wrongCol; // Change the button color to red for wrong answer
                }
            }
        }
    }
    
    // Reload the current scene for retrying the quiz
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reloads the current scene
    }

    // Update the UI to show the player has lost a life
    public void ReduceLives(int index)
    {
        lifeImageList[index].color = lostLifeCol; // Change the life image color to indicate a lost life
    }

    // Return to the quiz list by loading the previous scene
    public void ReturnToQuizList()
    {
        SceneManager.LoadScene("Quiz"); // Go back to the previous scene
    }
}
