using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Reflection;

public class UIPlantCatalogueItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image plantCardImage;
    [SerializeField] private TMP_Text plantCardName;
    // Start is called before the first frame update
    public event Action<UIPlantCatalogueItem> OnItemClicked; //LeftMouseClick equivalent for touch input later

    private bool empty;//When item is empty, nothing is done.

    public void Awake()
    {
        ResetData();
    }

    public void ResetData()
    {
        this.plantCardImage.gameObject.SetActive(false);
        empty = true;
    }

    public void SetData(Sprite sprite, string plantCardName)
    {
        this.plantCardImage.gameObject.SetActive(true);
        this.plantCardImage.sprite = sprite;
        this.plantCardName.text = plantCardName +"";
        empty = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        PointerEventData pointerData = (PointerEventData) data;
        Debug.Log("Click");
    }

    public void OnPointerUp(PointerEventData data)
    {   OnItemClicked?.Invoke(this);}
}
