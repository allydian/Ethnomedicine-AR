using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlantDescription : MonoBehaviour
{
    [SerializeField] private Image plantImage;
    [SerializeField] private TMP_Text plantName;
    [SerializeField] private TMP_Text plantDesc;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.plantImage.gameObject.SetActive(false);
        this.plantName.text = "";
        this.plantDesc.text = "";
    }

    public void SetDescription(Sprite sprite, string plantName, string plantDesc)
    {
        this.plantImage.gameObject.SetActive(true);
        this.plantImage.sprite = sprite;
        this.plantName.text = plantName;
        this.plantDesc.text = plantDesc;
    }
}
