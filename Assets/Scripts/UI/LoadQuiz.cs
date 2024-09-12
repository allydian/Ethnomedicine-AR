using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadQuiz : MonoBehaviour
{
    public void OpenMedicinalQuiz()
    {
        SceneManager.LoadScene("MedicinalPlantsQuiz");
    }

    public void OpenTreatmentsQuiz()
    {
        SceneManager.LoadScene("TreatmentsQuiz");
    }

    public void OpenFlashcardQuiz()
    {
        
    }

    public void ReturnToQuizList()
    {
        //SceneManager.LoadScene();
    }
}
