using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Localization;

/// <summary>
/// This class manages the display of a plant model in the UI.
/// It provides functionality to reset and display a specified plant model.
/// </summary>

public class UIPlantDescriptionModel : MonoBehaviour
{
    [SerializeField] private GameObject plantModel;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// This method ensures that the plant model is hidden upon initialization.
    /// </summary>
    public void Awake()
    {
        ResetModel();
    }

    /// <summary>
    /// Hides the current plant model by setting its active state to false.
    /// This can be called to reset or hide the plant model in the UI.
    /// </summary>
    public void ResetModel()
    {
        this.plantModel.gameObject.SetActive(false);
    }
    
    /// <param name="model">The new plant model GameObject to display in the UI.</param>
    public void DisplayPlantModel(GameObject model)
    {
        this.plantModel.gameObject.SetActive(true);
        this.plantModel = model;
    }
}
