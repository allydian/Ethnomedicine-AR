using UnityEngine;

[CreateAssetMenu(fileName = "ARQuestion", menuName = "Quiz/AR Question")]
public class ARQuizQuestionSO : ScriptableObject
{
    public string questionText;
    public int answerType; // e.g., ID of the correct answer
}