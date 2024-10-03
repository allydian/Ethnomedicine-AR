using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Notifies  and system that the class can be saved and store information to be implemented in the editor.
public class Question
{      
    public string questionInfo;
    public List<string> options;
    public int correctAns;
    public QuestionType questionType;
    public Sprite questionImg;
}