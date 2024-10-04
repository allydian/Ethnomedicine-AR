using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// VRHintManager is responsible for displaying plant information in a VR environment when the user interacts with specific points of interest.
/// It updates a UI panel with plant details (image, name, and description) and shows the panel on pointer interaction.
/// </summary>
[Serializable]
public class VRHintManager : MonoBehaviour, IPointerDownHandler
{
    /// <summary>
    /// Enum to represent different virtual reality locations where hints may be shown.
    /// </summary>
    public enum VRLocation
    {
        BNPI,   // Bako National Park I
        BNPII,  // Bako National Park II
        BNPIII  // Bako National Park III
    }

    public VRLocation location; // The current location in the VR environment where this hint manager is active.
    public GameObject plantInfoPanel; // The UI panel that displays the plant information.
    public Image plantImage; // The image component used to display the plant's image.
    public TMP_Text plantName; // The text component used to display the plant's name.
    public TMP_Text plantDesc; // The text component used to display the plant's description.

    public PlantSO plantSO; // The scriptable object containing data for the plant (image, name, description).

    /// <summary>
    /// Initializes the VRHintManager by updating the UI with plant information and hiding the info panel on start.
    /// </summary>
    void Start()
    {
        UpdateHintInfoPanel();
        plantInfoPanel.SetActive(false);
    }

    /// <summary>
    /// Handles pointer down events to show the plant information panel and update its content.
    /// </summary>
    /// <param name="pointerEventData">Data related to the pointer event (e.g., click).</param>
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        plantInfoPanel.SetActive(true);
        UpdateHintInfoPanel();
        Debug.Log("Pressed");
    }

    /// <summary>
    /// Updates the hint panel UI elements with the selected plant's image, name, and description.
    /// </summary>
    public void UpdateHintInfoPanel()
    {
        plantImage.sprite = plantSO.Image;
        plantName.text = plantSO.Name;
        plantDesc.text = plantSO.Description;
    }
}
