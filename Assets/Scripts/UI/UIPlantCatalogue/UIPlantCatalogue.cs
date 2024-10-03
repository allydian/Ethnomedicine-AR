using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the UI for a plant catalog, including displaying plant items,
/// handling plant selections, and showing plant descriptions.
/// </summary>
public class UIPlantCatalogue : MonoBehaviour
{
    [SerializeField] private UIPlantCatalogueItem plantPrefab; // Prefab reference for the plant item UI element. This will be instantiated for each plant in the catalog.
    [SerializeField] private RectTransform contentPanel; // Parent container (content panel) that holds all the instantiated plant catalog items.
    [SerializeField] private GameObject descPanel; // Panel that displays the plant's detailed description.
    [SerializeField] private UIPlantDescription plantDescription; // UI component that handles the display of the plant's image, name, and description.

    List<UIPlantCatalogueItem> listOfPlantItems = new List<UIPlantCatalogueItem>(); // List that stores all the UI plant items created in the catalog.




    public event Action<int> OnDescriptionRequested; // Event triggered when a plant's description is requested, passing the plant's index.
    public event Action<int> OnViewInARRequested; // Event triggered when a request to view the plant in AR is made, passing the plant's index.

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Hides the description panel and resets the plant description UI.
    /// </summary>
    private void Awake()
    {
        descPanel.SetActive(false);
        plantDescription.ResetDescription();
    }

    /// <summary>
    /// Initializes the plant catalog UI by creating plant catalog items based on the specified catalog size.
    /// Each plant item is instantiated from the plantPrefab and added to the contentPanel.
    /// </summary>
    /// <param name="catalogueSize">The number of plant items to create in the catalog.</param>
    public void InitialiseCatalogueUI(int catalogueSize)
    {
        for (int i = 0; i < catalogueSize; i++ )
        {
            UIPlantCatalogueItem uiPlant = Instantiate(plantPrefab, Vector3.zero,Quaternion.identity); // uiPlant is for each catalogued plant card in the UI in LearnPanel.
            uiPlant.transform.SetParent(contentPanel); // Add the plant item to the UI panel.
            listOfPlantItems.Add(uiPlant); // Add the plant item to the list for tracking.
            uiPlant.OnItemClicked += HandleItemSelection; // Subscribe to the item's click event for selecting the plant.
        }
    }

    /// <summary>
    /// Updates the data of a specific plant item in the catalog, such as its image and name.
    /// </summary>
    /// <param name="plantIndex">The index of the plant item to update.</param>
    /// <param name="plantImage">The image of the plant.</param>
    /// <param name="plantCardName">The name of the plant.</param>
    public void UpdateData(int plantIndex, Sprite plantImage, string plantCardName)
    {
        if (listOfPlantItems.Count > plantIndex)
        {
            listOfPlantItems[plantIndex].SetData(plantImage, plantCardName);
        }
    }

    /// <summary>
    /// Handles the selection of a plant item in the catalog.
    /// Displays the description panel and triggers the OnDescriptionRequested event.
    /// </summary>
    /// <param name="obj">The selected plant catalog item.</param>
    private void HandleItemSelection(UIPlantCatalogueItem obj)
    {
        descPanel.SetActive(true);

        int index = listOfPlantItems.IndexOf(obj);
        
        // If item is not on the list, return nothing. Otherwise, return the description.
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);

    }

    //Newly added method for requesting 3D model. Pending feature.
    public void HandleViewInARButtonClicked(int plantIndex)
    {
        OnViewInARRequested?.Invoke(plantIndex);
    }

    /// <summary>
    /// Shows the plant catalog UI and resets the plant description.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        plantDescription.ResetDescription();
    }

    /// <summary>
    /// Hides the plant catalog UI.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the plant description in the description panel with the specified data.
    /// </summary>
    /// <param name="plantIndex">The index of the plant to display in the description panel.</param>
    /// <param name="image">The image of the plant.</param>
    /// <param name="name">The name of the plant.</param>
    /// <param name="description">The description of the plant.</param>
    internal void UpdateDescription(int plantIndex, Sprite image, string name, string description)
    {
        plantDescription.SetDescription(image, name, description);
    }
}
