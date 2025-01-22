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
public class ARCameraManager : MonoBehaviour, IPointerDownHandler
{
    /// <summary>
    /// Enum to represent different virtual reality locations where hints may be shown.
    /// </summary>
    /*public enum VRLocation
    {
        BNPI,   // Bako National Park I
        BNPII,  // Bako National Park II
        BNPIII  // Bako National Park III
    }*/

    //public VRLocation location; // The current location in the VR environment where this hint manager is active.
    public GameObject compPanel; // The UI panel that displays the plant information.
    //public Image plantImage; // The image component used to display the plant's image.
    public TMP_Text compName; // The text component used to display the plant's name.
    public TMP_Text compDesc; // The text component used to display the plant's description.

    public ComponentSO componentSO; // The scriptable object containing data for the plant (image, name, description).
    //private CheckAchievements checkAchievements;  // Reference to CheckAchievements

    /// <summary>
    /// Initializes the VRHintManager by updating the UI with plant information and hiding the info panel on start.
    /// </summary>
    void Start()
    {
        UpdateHintInfoPanel();
        compPanel.SetActive(false);

        // Initialize the CheckAchievements script
        /*checkAchievements = FindObjectOfType<CheckAchievements>();
        if (checkAchievements == null)
        {
            Debug.LogError("CheckAchievements script not found!");
        }*/
    }

    /// <summary>
    /// Handles pointer down events to show the plant information panel and update its content.
    /// </summary>
    /// <param name="pointerEventData">Data related to the pointer event (e.g., click).</param>
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        compPanel.SetActive(true);
        UpdateHintInfoPanel();
        Debug.Log("Pressed");

        /*if (checkAchievements != null)
        {
            checkAchievements.CheckForestForager();
        }*/
    }

    /// <summary>
    /// Updates the hint panel UI elements with the selected plant's image, name, and description.
    /// </summary>
    public void UpdateHintInfoPanel()
    {
        compName.text = componentSO.Name;
        compDesc.text = componentSO.Description;
    }
}
