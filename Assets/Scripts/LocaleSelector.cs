using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private static LocaleSelector instance;
    private bool active = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }

    //private bool active = false;

    public void ChangeLocale(int localeID)
    {
        //if (active == true)
        if (active)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;

        // Find only TextMeshPro elements with a LocalizeStringEvent
        var localizedTextElements = FindObjectsOfType<UnityEngine.Localization.Components.LocalizeStringEvent>(true);

        // Temporarily clear only localized text elements to prevent flickering
        foreach (var localizedStringEvent in localizedTextElements)
        {
            var tmpText = localizedStringEvent.GetComponent<TMPro.TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = ""; // Clear text temporarily
            }
        }

        if (localeID >= 0 && localeID < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            PlayerPrefs.SetInt("LocaleKey", localeID);
            PlayerPrefs.Save(); // Ensures the data is saved immediately
        }

        yield return null; // Wait one frame to apply localization
        ForceUpdateUI(); // Refresh UI elements
        
        active = false;
    }

    private void ForceUpdateUI()
    {
        var localizedTextElements = FindObjectsOfType<UnityEngine.Localization.Components.LocalizeStringEvent>(true);

        foreach (var localizedStringEvent in localizedTextElements)
        {
            Debug.Log($"Refreshing localized text: {localizedStringEvent.gameObject.name}");
            localizedStringEvent.RefreshString();
        }
    }
}
