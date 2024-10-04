using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ScriptableObject that holds a list of questions for a quiz.
/// This allows the quiz data to be easily created and managed in the Unity Editor.
/// </summary>
[CreateAssetMenu(fileName = "QuestionData", menuName = "QuestionData", order = 1)]
public class QuizDataSO : ScriptableObject
{
    public List<Question> questions;  // List of questions used in the quiz.
}
