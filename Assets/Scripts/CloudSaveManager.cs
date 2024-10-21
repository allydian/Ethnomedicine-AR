using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Leaderboards;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;

public class CloudSaveManager : MonoBehaviour
{
    private const string quizScoreKey = "MedicinalPlantsQuizScore"; // Key for saving the quiz score

    private async void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        await UnityServices.InitializeAsync();
        await SignIn();
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    /// <summary>
    /// Signs in the user anonymously.
    /// </summary>
    private async Task SignIn()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in anonymously as: " + AuthenticationService.Instance.PlayerId);
        }
    }

    /// <summary>
    /// Saves the quiz score to Unity Cloud Save.
    /// </summary>
    /// <param name="score">The player's score to save.</param>
    public async Task SaveQuizScore(int score)
    {
        try
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { quizScoreKey, score }
            };

            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            Debug.Log("Quiz score saved to Cloud Save: " + score);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save score: " + e.Message);
        }
    }

    /// <summary>
    /// Retrieves the saved best score from Unity Cloud Save.
    /// </summary>
    public async Task<int> GetSavedBestScore()
    {
        try
        {
            var savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { quizScoreKey });
            if (savedData.TryGetValue(quizScoreKey, out string savedScoreString))
            {
                return int.Parse(savedScoreString);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error retrieving saved best score: " + e.Message);
        }
        return 0; // Default to 0 if no saved score is found
    }
    
    /// <summary>
    /// Submits the score to the Unity Leaderboard.
    /// </summary>
    /// <param name="score">The player's score to submit.</param>
    public async Task SubmitScoreToLeaderboard(int score)
    {
        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync("Medicinal-Plants-Leaderboard", score);
            Debug.Log("Score submitted to leaderboard: " + score);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to submit score to leaderboard: " + e.Message);
        }
    }
}
