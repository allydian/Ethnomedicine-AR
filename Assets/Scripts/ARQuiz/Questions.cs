using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions
{
    public string question = "";
    public int ansType;
    
    public Questions(string question, int ansType)
    {
        this.question = question;
        this.ansType = ansType;
    }
}