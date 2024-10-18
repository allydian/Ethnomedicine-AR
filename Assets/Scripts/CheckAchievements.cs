using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAchievements : MonoBehaviour
{
    private AchievementManager achievementManager;

    void Start()
    {
        // Initialize the AchievementManager instance
        achievementManager = AchievementManager.instance;
        if (achievementManager == null)
        {
            Debug.LogError("AchievementManager instance not found!");
        }
    }

    // Method to check and unlock the "Survivalist" achievement
    public void CheckSurvivalist(int livesRemaining)
    {
        if (livesRemaining == 3)
        {
            achievementManager.Unlock("Survivalist");
            Debug.Log("Survivalist achievement unlocked!");
        }
    }

    // Method to check and unlock the "Time Keeper" achievement
    public void CheckTimeKeeper(float currentTime, int livesRemaining)
    {
        if (currentTime > 0f && livesRemaining >= 1)
        {
            achievementManager.Unlock("TimeKeeper");
            Debug.Log("Time Keeper achievement unlocked!");
        }
    }

    public void CheckForestForager()
    {
        achievementManager.Unlock("ForestForager");
        Debug.Log("Forest Forager achievement unlocked!");
    }

    public void CheckCatalogueBrowser()
    {
        achievementManager.Unlock("CatalogueBrowser");
        Debug.Log("Catalogue Browser achievement unlocked!");
    }
}
