using UnityEngine;
using UnityEngine.Localization;
           
[CreateAssetMenu(fileName = "ARQuestion", menuName = "Quiz/AR Question")]
public class ARQuizQuestionSO : ScriptableObject
{
    //public string questionText;
    public LocalizedString localizedQuestionText;
    public int answerType; // e.g., ID of the correct answer
}