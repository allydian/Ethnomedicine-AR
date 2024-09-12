using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TMP_Text questionText, scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctCol, wrongCol, lostLifeCol, normalCol;

    private Question question;
    private bool answered;

    public TMP_Text ScoreText {get {return scoreText;} }
    public TMP_Text TimerText {get {return timerText;} }

    public GameObject GameOverPanel { get {return gameOverPanel;}}

    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
                Button localBtn = options[i];
                localBtn.onClick.AddListener(()=>OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.text:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.image:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.questionImg;
                break;
        }
        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TMP_Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        }

        answered = false;
    }

    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
    }

    private void OnClick(Button btn)
    {
        if(quizManager.GameStatus == GameStatus.Playing)
        {  if(!answered)
            {
                answered = true;
                bool questionIndex = quizManager.Answer(btn.name);

                if (questionIndex)
                {
                    btn.image.color = correctCol;
                }
                else
                {
                    btn.image.color = wrongCol;
                }
            }
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReduceLives(int index)
    {
        lifeImageList[index].color = lostLifeCol;
    }

    public void ReturnToQuizList()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
