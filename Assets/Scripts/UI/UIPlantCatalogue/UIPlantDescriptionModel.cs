using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlantDescriptionModel : MonoBehaviour
{
    [SerializeField] private GameObject plantModel;

    public void Awake()
    {
        ResetModel();
    }

    public void ResetModel()
    {
        this.plantModel.gameObject.SetActive(false);
    }

    public void DisplayPlantModel(GameObject model)
    {
        this.plantModel.gameObject.SetActive(true);
        this.plantModel = model;
    }
}
