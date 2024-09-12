using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu]

public class CatalogueSO : ScriptableObject
{
    [SerializeField] private List<PlantCatalogueItem> catalogueItems;
    [field: SerializeField] public int Size { get; private set; } = 10;

    public void Initialize()
    {
        catalogueItems = new List<PlantCatalogueItem>();
        for (int i = 0; i < Size; i++)
        {
            catalogueItems.Add(PlantCatalogueItem.GetEmptyPlantItem());
        }
    }

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

    public PlantCatalogueItem GetItemAt(int plantIndex)
    {
        return catalogueItems[plantIndex];
    }
}

[Serializable]
public struct PlantCatalogueItem
{
    public PlantSO plant;
    public string plantName;

    public bool IsEmpty => plant == null;

    public static PlantCatalogueItem GetEmptyPlantItem() => new PlantCatalogueItem
    {
        plant = null,
        plantName = "",
    };
}

