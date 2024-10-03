using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAchievementCard : MonoBehaviour
{

    [SerializeField] private TMP_Text achievementName;
    [SerializeField] private TMP_Text achievementDesc; 
    [SerializeField] private Image achievementBadge;

    private bool empty;

    public void SetData(string achievementName, string achievementDesc, Sprite achievementBadge)
    {
        this.achievementName.text = achievementName + "";
        this.achievementDesc.text = achievementDesc + "";
        this.achievementBadge.gameObject.SetActive(true);
        empty = false;
    }
}