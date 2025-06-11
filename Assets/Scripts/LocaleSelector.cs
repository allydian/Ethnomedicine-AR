using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LocaleSelector : MonoBehaviour
{
    private static LocaleSelector instance;
    private bool active = false;
    private string loadingSceneName = "LocalisationLoad";

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

/*
    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }
    */

    //private bool active = false;

    public void ChangeLocale(int localeID)
    {
        //if (active == true)
        //if (active)
        //    return;
        //StartCoroutine(SetLocale(localeID));
        if (active) return;
        StartCoroutine(ChangeLocaleWithLoadingScreen(localeID));
    }
    
    private IEnumerator ChangeLocaleWithLoadingScreen(int localeID)
    {
        active = true;
        
        // Load the loading scene additively
        yield return SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);
        
        // Get reference to the loading scene's root objects if needed
        Scene loadingScene = SceneManager.GetSceneByName(loadingSceneName);
        
        // Perform the locale change
        yield return StartCoroutine(SetLocale(localeID));
        
        // Wait a minimum time to ensure loading animation is visible (optional)
        yield return new WaitForSeconds(1f);
        
        // Unload the loading scene
        yield return SceneManager.UnloadSceneAsync(loadingSceneName);
        
        active = false;
    }

    private IEnumerator SetLocale(int localeID)
    {
        //active = true;
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


        ForceUpdateUI(); // Refresh UI elements

        //Debug.Log("Locale changed. Quitting application to apply changes on next launch.");


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
