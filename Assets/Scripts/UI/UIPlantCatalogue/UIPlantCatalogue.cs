using System;
using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class UIPlantCatalogue : MonoBehaviour
{
    [SerializeField] private UIPlantCatalogueItem plantPrefab; //Reference to plant prefab
    [SerializeField] private RectTransform contentPanel;

    [SerializeField] private GameObject descPanel;
    [SerializeField] private UIPlantDescription plantDescription;

    //Store indices of each plant card item created in UI
    List<UIPlantCatalogueItem> listOfPlantItems = new List<UIPlantCatalogueItem>();

    //public Sprite image;
    //public string plantName, plantDesc;

    public event Action<int> OnDescriptionRequested;
    public event Action<int> OnViewInARRequested; //Added for requesting 3D model

    private void Awake()
    {
        descPanel.SetActive(false);
        plantDescription.ResetDescription();
    }
    public void InitialiseCatalogueUI(int catalogueSize)
    {
        for (int i = 0; i < catalogueSize; i++ )
        {
            UIPlantCatalogueItem uiPlant = Instantiate(plantPrefab, Vector3.zero,Quaternion.identity); //uiPlant is for each catalogued plant card in the UI in LearnPanel.
            uiPlant.transform.SetParent(contentPanel); //contentPanel is the parent GameObject of each catalogued plant in the UI.
            listOfPlantItems.Add(uiPlant);//Adds to the list
            uiPlant.OnItemClicked += HandleItemSelection;//For selecting the plant
        }
    }

    public void UpdateData(int plantIndex, Sprite plantImage, string plantCardName)
    {
        if (listOfPlantItems.Count > plantIndex)
        {
            listOfPlantItems[plantIndex].SetData(plantImage, plantCardName);
        }
    }

    private void HandleItemSelection(UIPlantCatalogueItem obj)
    {
        descPanel.SetActive(true);
        //plantDescription.SetDescription(image, plantName, plantDesc);
        int index = listOfPlantItems.IndexOf(obj);
        //If item is not on the list, return nothing. Otherwise, return the description.
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);

    }

//Newly added method for requesting 3D model
    public void HandleViewInARButtonClicked(int plantIndex)
    {
        OnViewInARRequested?.Invoke(plantIndex);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        plantDescription.ResetDescription();

        //listOfPlantItems[0].SetData(image, plantName);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void UpdateDescription(int plantIndex, Sprite image, string name, string description)
    {
        plantDescription.SetDescription(image, name, description);
    }
}
