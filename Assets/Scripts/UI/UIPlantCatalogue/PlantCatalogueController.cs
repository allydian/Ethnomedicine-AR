using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class acts as a controller for managing interactions between the plant catalog UI and the catalog data.
/// It handles the initialization of the catalog, updating plant descriptions, and preparing the plant for AR viewing.
/// </summary>
public class PlantCatalogueController : MonoBehaviour
{
    [SerializeField] private UIPlantCatalogue catalogueUI; // UI component responsible for displaying the plant catalog.
    [SerializeField] private CatalogueSO catalogueData; // ScriptableObject that contains the plant catalog data.
    private CheckAchievements checkAchievements;  // Reference to CheckAchievements

    /// <summary>
    /// Called when the scene starts. Initializes the plant catalog UI and sets up event handlers.
    /// </summary>
    private void Start()
    {
        PrepareUI();
        
        checkAchievements = FindObjectOfType<CheckAchievements>();
        if (checkAchievements == null)
        {
            Debug.LogError("CheckAchievements script not found!");
        }
    }

    /// <summary>
    /// Prepares the UI by initializing the catalog based on the size of the catalog data and
    /// subscribes to the description request and AR view request events.
    /// </summary>
    public void PrepareUI()
    {
        catalogueUI.InitialiseCatalogueUI(catalogueData.Size); // Initialize the catalog UI with the size of the data in the catalogueData
        
        this.catalogueUI.OnDescriptionRequested += HandleDescriptionRequest;  // Subscribe to the events for handling description requests
        this.catalogueUI.OnViewInARRequested += HandleViewInARRequest; // Subscribe to the events for handling AR viewing
    }

    /// <summary>
    /// Handles the event triggered when a plant description is requested.
    /// It fetches the relevant plant data from the catalog and updates the UI with the plant's details.
    /// </summary>
    /// <param name="plantIndex">The index of the requested plant in the catalog.</param>
    private void HandleDescriptionRequest(int plantIndex)
    {
        PlantCatalogueItem plantCatalogueItem = catalogueData.GetItemAt(plantIndex);

        if(plantCatalogueItem.IsEmpty)
            return;

        PlantSO plant = plantCatalogueItem.plant;

        catalogueUI.UpdateDescription(plantIndex, plant.Image, plant.Name, plant.Description); // Update the description panel in the UI with the selected plant's details

        if (checkAchievements != null)
        {
            checkAchievements.CheckCatalogueBrowser();
        }
    }

    /// <summary>
    /// Handles the event triggered when a request is made to view a plant in AR.
    /// It sets the selected plant's prefab for the AR scene and loads the AR scene.
    /// </summary>
    /// <param name="plantIndex">The index of the requested plant in the catalog.</param>
    private void HandleViewInARRequest(int plantIndex)
    {
        PlantCatalogueItem plantCatalogueItem = catalogueData.GetItemAt(plantIndex);
       
        if (plantCatalogueItem.IsEmpty)
            return;

        PlantSO plant = plantCatalogueItem.plant;

        // Set the selected plant prefab for the AR scene
        ARSceneManager.SelectedPlantPrefab = plant.Model;

        // Load the AR scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ViewPlantinAR");
    }

    /// <summary>
    /// Updates the UI with the current state of the catalog data.
    /// Loops through the catalog items and updates the catalog UI with the plant data (image and name).
    /// </summary>
    public void Update()
    {
        foreach (var plant in catalogueData.GetCurrentCatalogueState())
        {
            catalogueUI.UpdateData(plant.Key, plant.Value.plant.Image, plant.Value.plant.Name); // Update the plant catalog UI with the plant's image and name for each catalog item
        }
    }
}
