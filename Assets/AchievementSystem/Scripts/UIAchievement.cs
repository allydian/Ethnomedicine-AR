using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization; // Add Localization namespace
using UnityEngine.Localization.Components; // Add Localization Components namespace

/// <summary>
/// Defines the logic behind a single achievement on the UI
/// </summary>
public class UIAchievement : MonoBehaviour
{
    public TMP_Text Title, Description, Percent;
    public Image Icon, OverlayIcon, ProgressBar;
    public GameObject SpoilerOverlay;
    public Text SpoilerText;
    [HideInInspector]public AchievenmentStack AS;

    private LocalizedString localizedTitle;
    private LocalizedString localizedDescription;
    //[SerializeField] private LocalizedString localizedTitle;
    //[SerializeField] private LocalizedString localizedDescription;
    public LocalizedString localizedSpoilerMessage;
    public LocalizedString localizedAchievedText;
    public LocalizedString localizedLockedText;

    /// <summary>
    /// Destroy object after a certain amount of time
    /// </summary>
    public void StartDeathTimer ()
    {
        StartCoroutine(Wait());
    }
/*
    /// <summary>
    /// Add information  about an Achievement to the UI elements
    /// </summary>
    public void Set (AchievementInfromation Information, AchievementState State)
    {
        if(Information.Spoiler && !State.Achieved)
        {
            SpoilerOverlay.SetActive(true);
            SpoilerText.text = AchievementManager.instance.SpoilerAchievementMessage;
        }
        else
        {
            Title.text = Information.DisplayName;
            Description.text = Information.Description;

            if (Information.LockOverlay && !State.Achieved)
            {
                OverlayIcon.gameObject.SetActive(true);
                OverlayIcon.sprite = Information.LockedIcon;
                Icon.sprite = Information.AchievedIcon;
            }
            else
            {
                Icon.sprite = State.Achieved ? Information.AchievedIcon : Information.LockedIcon;
            }

            if (Information.Progression)
            {
                float CurrentProgress = AchievementManager.instance.ShowExactProgress ? State.Progress : (State.LastProgressUpdate * Information.NotificationFrequency);
                float DisplayProgress = State.Achieved ? Information.ProgressGoal : CurrentProgress;

                if (State.Achieved)
                {
                    Percent.text = Information.ProgressGoal + Information.ProgressSuffix + " / " + Information.ProgressGoal + Information.ProgressSuffix + " (Achieved)";
                }
                else
                {
                    Percent.text = DisplayProgress + Information.ProgressSuffix +  " / " + Information.ProgressGoal + Information.ProgressSuffix;
                }

                ProgressBar.fillAmount = DisplayProgress / Information.ProgressGoal;
            }
            else //Single Time
            {
                ProgressBar.fillAmount = State.Achieved ? 1 : 0;
                Percent.text = State.Achieved ? "(Achieved)" : "(Locked)";
            }
        }
    }*/

    public void Set(AchievementInformation Information, AchievementState State)
    {
        if (Information.Spoiler && !State.Achieved)
        {
            SpoilerOverlay.SetActive(true);
            // Use localized spoiler message
            localizedSpoilerMessage.StringChanged += UpdateSpoilerText;
            localizedSpoilerMessage.TableEntryReference = "AchievementMessages"; // Set the table and entry
        }
        else
        {
            // Use localized title and description
            //localizedTitle.StringChanged += UpdateTitleText;
            //localizedDescription.StringChanged += UpdateDescriptionText;
            localizedTitle = Information.LocalizedTitleKey;
            localizedDescription = Information.LocalizedDescriptionKey;

            //localizedTitle.TableEntryReference = Information.LocalizedTitleKey; // Set the table and entry
            //localizedDescription.TableEntryReference = Information.LocalizedDescriptionKey; // Set the table and entry
 
            localizedTitle = Information.LocalizedTitleKey;
            localizedDescription = Information.LocalizedDescriptionKey;

            localizedTitle.StringChanged += UpdateTitleText;
            localizedDescription.StringChanged += UpdateDescriptionText;

            UpdateTitleText(localizedTitle.GetLocalizedString());
            UpdateDescriptionText(localizedDescription.GetLocalizedString());

            if (Information.LockOverlay && !State.Achieved)
            {
                OverlayIcon.gameObject.SetActive(true);
                OverlayIcon.sprite = Information.LockedIcon;
                Icon.sprite = Information.AchievedIcon;
            }
            else
            {
                Icon.sprite = State.Achieved ? Information.AchievedIcon : Information.LockedIcon;
            }

            if (Information.Progression)
            {
                float CurrentProgress = AchievementManager.instance.ShowExactProgress ? State.Progress : (State.LastProgressUpdate * Information.NotificationFrequency);
                float DisplayProgress = State.Achieved ? Information.ProgressGoal : CurrentProgress;

                if (State.Achieved)
                {
                    //Percent.text = Information.ProgressGoal + Information.ProgressSuffix + " / " + Information.ProgressGoal + Information.ProgressSuffix + " (Achieved)";
                    localizedAchievedText.StringChanged += (value) => Percent.text = $"{Information.ProgressGoal}{Information.ProgressSuffix} / {Information.ProgressGoal}{Information.ProgressSuffix} ({value})";
                    localizedAchievedText.GetLocalizedString();
                }
                else
                {
                    Percent.text = DisplayProgress + Information.ProgressSuffix + " / " + Information.ProgressGoal + Information.ProgressSuffix;
                }

                ProgressBar.fillAmount = DisplayProgress / Information.ProgressGoal;
            }
            else // Single Time
            {
                //ProgressBar.fillAmount = State.Achieved ? 1 : 0;
                //Percent.text = State.Achieved ? "(Achieved)" : "(Locked)";
                ProgressBar.fillAmount = State.Achieved ? 1 : 0;
                if (State.Achieved)
                {
                    localizedAchievedText.StringChanged += (value) => Percent.text = value;
                    localizedAchievedText.GetLocalizedString();
                }
                else
                {
                    localizedLockedText.StringChanged += (value) => Percent.text = value;
                    localizedLockedText.GetLocalizedString();
                }
            }
        }
    }

    // Update localized title text
    private void UpdateTitleText(string value)
    {
        Title.text = value;
    }

    // Update localized description text
    private void UpdateDescriptionText(string value)
    {
        Description.text = value;
    }

    // Update localized spoiler text
    private void UpdateSpoilerText(string value)
    {
        SpoilerText.text = value;
    }

    private void OnDestroy()
    {
        if (localizedTitle != null) localizedTitle.StringChanged -= UpdateTitleText;
        if (localizedDescription != null) localizedDescription.StringChanged -= UpdateDescriptionText;
        if (localizedSpoilerMessage != null) localizedSpoilerMessage.StringChanged -= UpdateSpoilerText;
    }

    private IEnumerator Wait ()
    {
        yield return new WaitForSeconds(AchievementManager.instance.DisplayTime);
        GetComponent<Animator>().SetTrigger("ScaleDown");
        yield return new WaitForSeconds(0.1f);
        AS.CheckBackLog();
        Destroy(gameObject);
    }
}
