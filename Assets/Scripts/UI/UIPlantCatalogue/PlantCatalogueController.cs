using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCatalogueController : MonoBehaviour
{
    [SerializeField] private UIPlantCatalogue catalogueUI;
    [SerializeField] private CatalogueSO catalogueData;

    //int catalogueSize = 10;
    private void Start()
    {
        //catalogueUI.InitialiseCatalogueUI(catalogueSize);
        //catalogueUI.InitialiseCatalogueUI(catalogueData.Size);
        //catalogueData.Initialize();
        PrepareUI();
    }

    public void PrepareUI()
    {
        catalogueUI.InitialiseCatalogueUI(catalogueData.Size);
        this.catalogueUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.catalogueUI.OnViewInARRequested += HandleViewInARRequest;
    }

    private void HandleDescriptionRequest(int plantIndex)
    {
        PlantCatalogueItem plantCatalogueItem = catalogueData.GetItemAt(plantIndex);
        if(plantCatalogueItem.IsEmpty)
            return;
        PlantSO plant = plantCatalogueItem.plant;
        catalogueUI.UpdateDescription(plantIndex, plant.Image, plant.Name, plant.Description);
    }

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

    public void Update()
    {
        foreach (var plant in catalogueData.GetCurrentCatalogueState())
        {
            catalogueUI.UpdateData(plant.Key, plant.Value.plant.Image, plant.Value.plant.Name);
        }
    }
}
