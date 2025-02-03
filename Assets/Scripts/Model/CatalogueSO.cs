using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu]
/// <summary>
/// This ScriptableObject represents a catalogue of plants that can be initialized and manipulated. 
/// It stores a collection of PlantCatalogueItems and provides methods to add plants and retrieve catalogue state.
/// </summary>
public class CatalogueSO : ScriptableObject
{
    [SerializeField] private List<PlantCatalogueItem> catalogueItems; // A list of PlantCatalogueItems representing the catalogue.
    [field: SerializeField] public int Size { get; private set; } = 10; // Defines the maximum size of the catalogue. Default value is 10.

    /// <summary>
    /// Initializes the catalogue with empty plant items up to the defined size.
    /// </summary>
    public void Initialize()
    {
        catalogueItems = new List<PlantCatalogueItem>();
        for (int i = 0; i < Size; i++)
        {
            catalogueItems.Add(PlantCatalogueItem.GetEmptyPlantItem());
        }
    }

    /// <summary>
    /// Adds a plant to the first available empty slot in the catalogue.
    /// </summary>
    /// <param name="plant">The PlantSO to add to the catalogue.</param>
    /// <param name="plantName">The name of the plant to display in the catalogue.</param>
    /*
    public void AddPlant(PlantSO plant, string plantName)
    {
        for (int i = 0; i < catalogueItems.Count; i++)
        {
            if(catalogueItems[i].IsEmpty)
            {
                catalogueItems[i] = new PlantCatalogueItem
                {
                    plant = plant,
                    plantName = plantName,
                };
            }
        }
    }
    */

    public void AddPlant(PlantSO plant, LocalizedString localizedPlantName)
    {
        for (int i = 0; i < catalogueItems.Count; i++)
        {
            if(catalogueItems[i].IsEmpty)
            {
                catalogueItems[i] = new PlantCatalogueItem
                {
                    plant = plant,
                    plantName = localizedPlantName,
                };
            }
        }
    }
    /// <summary>
    /// Retrieves the current state of the catalogue, excluding any empty slots.
    /// </summary>
    /// <returns>A dictionary with the index as the key and the PlantCatalogueItem as the value.</returns>
    public Dictionary<int, PlantCatalogueItem> GetCurrentCatalogueState()
    {
        Dictionary<int, PlantCatalogueItem> returnValue = new Dictionary<int, PlantCatalogueItem>();
        for (int i = 0; i < catalogueItems.Count; i++)
        {
            if (catalogueItems[i].IsEmpty)
                continue;
            returnValue[i] = catalogueItems[i];
        }
        return returnValue;
    }

    /// <summary>
    /// Gets the PlantCatalogueItem at the specified index.
    /// </summary>
    /// <param name="plantIndex">The index of the plant in the catalogue.</param>
    /// <returns>The PlantCatalogueItem at the specified index.</returns>
    public PlantCatalogueItem GetItemAt(int plantIndex)
    {
        return catalogueItems[plantIndex];
    }
}

/// <summary>
/// Represents an individual item in the plant catalogue, containing a plant and its name.
/// </summary>
[Serializable]
public struct PlantCatalogueItem
{
    public PlantSO plant; // The plant associated with this catalogue item.
    //public string plantName; // The name of the plant.
    public LocalizedString plantName;

    public bool IsEmpty => plant == null; // Checks if this catalogue item is empty (i.e., the plant is null).

    /// <summary>
    /// Returns an empty plant item to represent a slot without a plant.
    /// </summary>
    /// <returns>A new PlantCatalogueItem with a null plant and empty plantName.</returns>
    public static PlantCatalogueItem GetEmptyPlantItem() => new PlantCatalogueItem 
    {
        plant = null,
        //localizedPlantName = "",
        plantName = new LocalizedString(),
    };
}

