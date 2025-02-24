using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.Localization;

/// <summary>
/// This class represents an individual plant item in the UI plant catalog.
/// It handles displaying the plant's image and name, and triggers an event when clicked or tapped.
/// </summary>
public class UIPlantCatalogueItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// The UI element used to display the plant's image & name in the card.
    /// Serialized to be assigned via the Unity Editor.
    /// </summary>
    [SerializeField] private Image plantCardImage;
    [SerializeField] private TMP_Text plantCardName;

    public event Action<UIPlantCatalogueItem> OnItemClicked; // Used to be subscribed to handle interactions with the plant catalog item.
    private bool empty; // When the item is empty, nothing is done.

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Ensures that the catalog item is reset to its empty state upon initialization.
    /// </summary>
    public void Awake()
    {
        ResetData();
    }

    /// <summary>
    /// Resets the catalog item by hiding the plant image and marking the item as empty.
    /// This is typically called when the item has no data to display.
    /// </summary>
    public void ResetData()
    {
        this.plantCardImage.gameObject.SetActive(false);
        empty = true;
    }

    /// <summary>
    /// Sets the plant data for the catalog item.
    /// Displays the provided plant image and name, and marks the item as not empty.
    /// </summary>
    /// <param name="sprite">The sprite to be used as the plant's image.</param>
    /// <param name="plantCardName">The name of the plant to be displayed.</param>
    /*public void SetData(Sprite sprite, string plantCardName)
    {
        this.plantCardImage.gameObject.SetActive(true);
        this.plantCardImage.sprite = sprite;
        this.plantCardName.text = plantCardName +"";
        empty = false;
    }*/

    public void SetData(Sprite sprite, LocalizedString plantCardName) // Updated to use LocalizedString
    {
        this.plantCardImage.gameObject.SetActive(true);
        this.plantCardImage.sprite = sprite;

        // Fetch localized string asynchronously
        plantCardName.GetLocalizedStringAsync().Completed += (operation) =>
        {
            this.plantCardName.text = operation.Result;
        };

        empty = false;
    }

    /// <summary>
    /// Handles the pointer down event when the user presses or taps on the catalog item.
    /// This is part of the IPointerDownHandler interface.
    /// </summary>
    /// <param name="data">Pointer event data for the press or tap.</param>
    public void OnPointerDown(PointerEventData data)
    {
        PointerEventData pointerData = (PointerEventData) data;
        Debug.Log("Click");
    }

    /// <summary>
    /// Handles the pointer up event when the user releases or lifts a tap on the catalog item.
    /// It triggers the OnItemClicked event, allowing subscribers to react to the interaction.
    /// This is part of the IPointerUpHandler interface.
    /// </summary>
    /// <param name="data">Pointer event data for the release or lift.</param>
    public void OnPointerUp(PointerEventData data)
    {   OnItemClicked?.Invoke(this);}
}
