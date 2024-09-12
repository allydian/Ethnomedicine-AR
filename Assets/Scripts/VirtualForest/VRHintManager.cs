using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.Video;
/*
This script is specifically for calling methods when the GameObject it is attached to is pressed on the screen.
1. Using enum and switch case, does SetInt to its respective declared key in the PlayerPrefs.
2. Updates the appearance of its respective hint sprite in the hint counter according to ScriptableObject.
3. Updates the content of its respective hint info panel according to ScriptableObject.
4. Updates and saves the hint counter with PlayerPrefs so that it will be persistent across scenes and sessions.
*/

[Serializable]
public class VRHintManager : MonoBehaviour, IPointerDownHandler
{
    public enum VRLocation{
        BNPI, BNPII, BNPIII
    }

    public VRLocation location;

    //UI Elements of the panel
    public GameObject plantInfoPanel;
    public Image plantImage;
    public TMP_Text plantName;
    public TMP_Text plantDesc;
    //public VideoClip hintVideo;

    //ScriptableObject
    public PlantSO plantSO;

    void Start(){
        //CheckHintStatus();
        UpdateHintInfoPanel();
        plantInfoPanel.SetActive(false);
    }

    public void OnPointerDown(PointerEventData pointerEventData){
        /*switch(location){
            case VRLocation.KCHWaterfront:
                //PlayerPrefs.SetInt("KCHWaterfront1",1);
                plantIcon.sprite = plantSO.plantFound;
                break;
            case VRLocation.KCHWaterfront2:
                PlayerPrefs.SetInt("KCHWaterfront2",1);
                hintIcon.sprite = hintSO.hintFound;
                break;
            case VRLocation.TamanPerpaduan1:
                PlayerPrefs.SetInt("TamanPerpaduan1",1);
                hintIcon.sprite = hintSO.hintFound;
                break;
            case VRLocation.TamanPerpaduan2:
                PlayerPrefs.SetInt("TamanPerpaduan2",1);
                hintIcon.sprite = hintSO.hintFound;
                break;
        }*/
        plantInfoPanel.SetActive(true);
        //PlayerPrefs.Save();
        UpdateHintInfoPanel();
        Debug.Log("Pressed");
    }

    public void UpdateHintInfoPanel(){
        plantImage.sprite = plantSO.Image;
        plantName.text = plantSO.Name;
        plantDesc.text = plantSO.Description;
    }
/*
    void CheckHintStatus(){
        int kchWaterfrontCounter = PlayerPrefs.GetInt("KCHWaterfrontCounter");
        int tmnPerpaduanCounter = PlayerPrefs.GetInt("TamanPerpaduanCounter");
        int kchWaterfront1S = PlayerPrefs.GetInt("KCHWaterfront1");
        int kchWaterfront2S = PlayerPrefs.GetInt("KCHWaterfront2");
        int tmnPerpaduan1S = PlayerPrefs.GetInt("TamanPerpaduan1");
        int tmnPerpaduan2S = PlayerPrefs.GetInt("TamanPerpaduan2");

        switch(location){
            case VRLocation.KCHWaterfront1:
                if(kchWaterfront1S == 1){
                    hintIcon.sprite = hintSO.hintFound;
                    PlayerPrefs.SetInt("KCHWaterfrontCounter", PlayerPrefs.GetInt("KCHWaterfrontCounter") + 1);
                }else{
                    hintIcon.sprite = hintSO.hintNotFound;
                }
                break;
            case VRLocation.KCHWaterfront2:
                if(kchWaterfront2S == 1){
                    hintIcon.sprite = hintSO.hintFound;
                    PlayerPrefs.SetInt("KCHWaterfrontCounter", PlayerPrefs.GetInt("KCHWaterfrontCounter") + 1);
                }else{
                    hintIcon.sprite = hintSO.hintNotFound;
                }
                break;
            case VRLocation.TamanPerpaduan1:
                if(tmnPerpaduan1S == 1){
                    hintIcon.sprite = hintSO.hintFound;
                    PlayerPrefs.SetInt("TamanPerpaduanCounter", PlayerPrefs.GetInt("TamanPerpaduanCounter") + 1);
                }else{
                    hintIcon.sprite = hintSO.hintNotFound;
                }
                break;
            case VRLocation.TamanPerpaduan2:
                if(tmnPerpaduan2S == 1){
                    hintIcon.sprite = hintSO.hintFound;
                    PlayerPrefs.SetInt("TamanPerpaduanCounter", PlayerPrefs.GetInt("TamanPerpaduanCounter") + 1);
                }else{
                    hintIcon.sprite = hintSO.hintNotFound;
                }
                break;
        }

        PlayerPrefs.Save();
    }*/
}
