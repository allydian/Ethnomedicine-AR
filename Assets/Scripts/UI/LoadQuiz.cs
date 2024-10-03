using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A class responsible for loading different quiz scenes in the application.
/// This class provides methods to load specific quiz scenes when called.
/// </summary>
public class LoadQuiz : MonoBehaviour
{
    // Loads the scene for the Medicinal Plants Quiz.
    public void OpenMedicinalQuiz()
    {
        SceneManager.LoadScene("MedicinalPlantsQuiz");
    }

    // Loads the scene for the Treatments Quiz.
    public void OpenTreatmentsQuiz()
    {
        SceneManager.LoadScene("TreatmentsQuiz");
    }
}
