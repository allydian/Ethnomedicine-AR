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

    private string leaderboardID = "Medicinal-Plants-Leaderboard";  // Leaderboard ID from Unity Dashboard
    
    private CloudSaveManager cloudSaveManager;  // Reference to CloudSaveManager


    private void Start()
    {
        cloudSaveManager = FindObjectOfType<CloudSaveManager>();

        // Check if CloudSaveManager is assigned
        if (cloudSaveManager == null)
        {
            Debug.LogError("CloudSaveManager not assigned in LeaderboardsManager!");
        }

        leaderboardParent.SetActive(false);
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
                leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = rank.ToString();  // Player rank
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
            }

            await Task.Delay(500);
        }
    }


    public async Task<string> GetDisplayName(string userId)
    {
        try
        {
            LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);

            foreach (LeaderboardEntry entry in leaderboardScoresPage.Results)
            {
                if (entry.PlayerId == userId)
                {
                    return entry.PlayerName;  // Return the player's display name
                }
            }
            await SubmitDefaultScoreIfNeeded();
        }
        catch (LeaderboardsException e) when (e.Reason == LeaderboardsExceptionReason.ScoreSubmissionRequired)
        {
            // Handle the "Score submission required" error by submitting a default score
            await SubmitDefaultScoreIfNeeded();
            return await GetDisplayName(userId); // Retry after submitting default score
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to retrieve display name from leaderboard: " + e.Message);
        }

        return null;  // Return null if name not found
    }

    private async Task SubmitDefaultScoreIfNeeded()
    {
        try
        {
            // Submit a score of 0 to allow the player to view the leaderboard. This 0 serves as teh default score.
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, 0);
            Debug.Log("Default score submitted for new user to allow leaderboard access.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to submit default score: " + e.Message);
        }
    }
}