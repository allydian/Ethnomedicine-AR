using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class handles the display of a plant's description in the UI,
/// including its image, name, and description text.
/// </summary>
public class UIPlantDescription : MonoBehaviour
{
    /// <summary>
    /// The UI element used to display the plant's image.
    /// Serialized to be assigned via the Unity Editor.
    /// </summary>
    [SerializeField] private Image plantImage;
    [SerializeField] private TMP_Text plantName;
    [SerializeField] private TMP_Text plantDesc;

    /// <summary>
    /// The UI element used to display the plant's name.
    /// Serialized to be assigned via the Unity Editor.
    /// </summary>
    public void Awake()
    {
        ResetDescription();
    }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Ensures that the plant description UI is reset and hidden upon initialization.
    /// </summary>
    public void ResetDescription()
    {
        this.plantImage.gameObject.SetActive(false); //Disables UI element that displays the plantImage
        this.plantName.text = ""; // Resets name
        this.plantDesc.text = ""; // Resets description
    }

    /// <summary>
    /// Resets the plant description UI elements.
    /// Hides the plant image and clears the name and description text fields.
    /// </summary>
    public void SetDescription(Sprite sprite, string plantName, string plantDesc)
    {
        this.plantImage.gameObject.SetActive(true); // Enables UI element that displays plantImage
        this.plantImage.sprite = sprite; // Sets the plant's image sprite
        this.plantName.text = plantName; // Sets the plant's name
        this.plantDesc.text = plantDesc; // Sets the plant's description
    }
}
