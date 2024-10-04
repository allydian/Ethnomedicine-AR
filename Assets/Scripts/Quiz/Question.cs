using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a quiz question that contains the question text, options, correct answer index, type of question, and an optional image.
/// </summary>
[System.Serializable]
public class Question
{      
    public string questionInfo; // The main question or prompt that is being asked.
    public List<string> options; // A list of possible answers/options for the question.
    public int correctAns; // The index of the correct answer in the options list.
    public QuestionType questionType; // The type of question, which can be either text-based or image-based.
    public Sprite questionImg; // An optional image associated with the question, only used if the questionType is set to "image".
}