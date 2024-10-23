using System.Collections;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using Unity.Services.Leaderboards.Exceptions;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardParent;
    [SerializeField] private Transform leaderboardContentParent;
    [SerializeField] private Transform leaderboardItemPrefab;

    [SerializeField] private Color currentUserHighlightColor = Color.red;
    //[SerializeField] private Sprite bronzeTierSprite, silverTierSprite, goldenTierSprite;

    private string leaderboardID = "Medicinal-Plants-Leaderboard";  // Your leaderboard ID
    
    private CloudSaveManager cloudSaveManager;  // Reference to CloudSaveManager
    

    private async void Start()
    {
        //await UnityServices.InitializeAsync();
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
        cloudSaveManager = FindObjectOfType<CloudSaveManager>();

        // Check if CloudSaveManager is assigned
        if (cloudSaveManager == null)
        {
            Debug.LogError("CloudSaveManager not assigned in LeaderboardsManager!");
        }

        //leaderboardParent.SetActive(false);
    }

    // Method to show the leaderboard when triggered by a UI button
    public async void ShowLeaderboard()
    {
        leaderboardParent.SetActive(true);  // Activate the leaderboard UI

        // Fetch and update the leaderboard content
        await UpdateLeaderboard();
    }

    // Method to hide the leaderboard (you can trigger this from another button if needed)
    public void HideLeaderboard()
    {
        leaderboardParent.SetActive(false);  // Deactivate the leaderboard UI
    }

    /*
    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (leaderboardParent.activeInHierarchy)
            {
                leaderboardParent.SetActive(false);
            }
            else
            {
                leaderboardParent.SetActive(true);
                UpdateLeaderboard();

                // Fetch the score from CloudSaveManager and submit it to the leaderboard
                if (cloudSaveManager != null)
                {
                    int savedBestScore = await cloudSaveManager.GetSavedBestScore();  // Retrieve score from cloud save

                    try
                    {
                        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, savedBestScore);
                        Debug.Log("Score submitted to leaderboard: " + savedBestScore);
                    }
                    catch (LeaderboardsException e)
                    {
                        Debug.LogError("Failed to submit score: " + e.Reason);
                    }
                }
            }
        }
    }
    */

    //    private async void UpdateLeaderboard()
    private async Task UpdateLeaderboard()
    {
        while (Application.isPlaying && leaderboardParent.activeInHierarchy)
        {
            LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);

            foreach (Transform t in leaderboardContentParent)
            {
                Destroy(t.gameObject);
            }

            int rank = 1;
            string currentUserId = AuthenticationService.Instance.PlayerId;

            foreach (LeaderboardEntry entry in leaderboardScoresPage.Results)
            {
                Transform leaderboardItem = Instantiate(leaderboardItemPrefab, leaderboardContentParent);
                //string playerName = entry.PlayerId; // Use PlayerID as the "name"
                leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = rank.ToString();  // Player rank
                //leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerName;       // PlayerID as Player Name
                leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.PlayerName;       // PlayerID as Player Name
                leaderboardItem.GetChild(2).GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();  // Player score
                
                // If the entry belongs to the current signed-in player, apply the highlight color
                if (entry.PlayerId == currentUserId)
                {
                    leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().color = currentUserHighlightColor;  // Highlight rank
                    leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().color = currentUserHighlightColor;  // Highlight player name
                    leaderboardItem.GetChild(2).GetComponent<TextMeshProUGUI>().color = currentUserHighlightColor;  // Highlight score
                }

                rank++;

                /*
                Sprite tierSprite = null;
                switch (entry.Tier)
                {
                    case "Bronze_tier":
                        tierSprite = bronzeTierSprite;
                        break;
                    case "Silver_tier":
                        tierSprite = silverTierSprite;
                        break;
                    case "Golden_tier":
                        tierSprite = goldenTierSprite;
                        break;
                }

                leaderboardItem.GetChild(2).GetComponent<Image>().sprite = tierSprite;
                */
            }

            await Task.Delay(500);
        }
    }
}