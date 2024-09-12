using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSceneManager : MonoBehaviour
{
    public static GameObject SelectedPlantPrefab;

    void Start()
    {
        if (SelectedPlantPrefab != null)
        {
            // Instantiate the selected plant model at the desired location
            Instantiate(SelectedPlantPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
