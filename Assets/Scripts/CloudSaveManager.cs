//Optimise further by centralising all user authentication-dependent items on a separate script (UserAuthManager.cs)
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Leaderboards;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class CloudSaveManager : MonoBehaviour
{
    public TMP_Text userNameText;  // Assign this in the Inspector
    public TMP_Text userIdText;    // Assign this in the Inspector

    private LeaderboardManager leaderboardManager;

    private const string quizScoreKey = "MedicinalPlantsQuizScore"; // Key for saving the quiz score

    private async void Start()
    {
        DontDestroyOnLoad(this.gameObject); // Keeps the CloudSaveManager static throughout different scenes.
        await UnityServices.InitializeAsync(); // Initializes Unity Services (e.g., Cloud Save, Authentication, etc.)
        await SignIn(); // Signs the player in

        // Finds the LeaderboardManager in the scene, which handles leaderboard functionality.
        // This code can be modified later if account registration is implemented
        leaderboardManager = FindObjectOfType<LeaderboardManager>();
        
        await DisplayUserInfo();  // Displays the user information (e.g., username, score, etc.)

        var achievementsData = await LoadAchievementsData();  // Loads achievement data. May delete if redundant.
        Debug.Log("Achievements loaded after initialization: " + achievementsData.Count); // Logs the number of achievements loaded after initialization to verify successful data load
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
    /// This method asynchronously retrieves and displays user information. It first fetches the player's unique ID 
    /// from the Authentication service and displays it in the `userIdText` UI element. Then, it uses the 
    /// `LeaderboardManager` to get the player's display name based on the user ID. If the `LeaderboardManager` 
    /// is available, the display name is shown in the `userNameText` UI element, or a fallback message ("Name not found") 
    /// is shown if the name is empty. If the `LeaderboardManager` is not found, an error message is logged.
    /// </summary>
    private async Task DisplayUserInfo()
    {
        string userId = AuthenticationService.Instance.PlayerId;
        userIdText.text = $"ID: {userId}";

         // Use LeaderboardManager to get the display name
        if (leaderboardManager != null)
        {
            string displayName = await leaderboardManager.GetDisplayName(userId);
            userNameText.text = !string.IsNullOrEmpty(displayName) ? displayName : "Name not found";
        }
        else
        {
            Debug.LogError("LeaderboardManager not found!");
        }
    }

    /// <summary>
    /// Deletes the user account from Unity Gaming Services.
    /// </summary>
    public async Task DeleteAccount()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            try
            {
                await AuthenticationService.Instance.DeleteAccountAsync();
                Debug.Log("User account deleted successfully.");
                ReloadGame();
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to delete account: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("User is not signed in.");
        }
    }

    // Wrapper method to call from the Inspector
    public void DeleteAccountButtonPressed()
    {
        _ = DeleteAccount();  // Using the async method without waiting for it
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0); // Load the initial scene (Scene 0 in the build settings)
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

    /// <summary>
    /// Asynchronously saves the provided achievements data to Cloud Save.
    /// This method attempts to save the achievements data, and logs a success message if successful.
    /// If the save operation fails, an error message is logged.
    /// </summary>
    /// <param name="achievementsData">A dictionary containing the achievements data to be saved, where keys are achievement identifiers, and values are the achievement data.</param>
    public async Task SaveAchievementsData(Dictionary<string, object> achievementsData)
    {
        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(achievementsData);
            Debug.Log("Achievements saved to Cloud Save.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save achievements: " + e.Message);
        }
    }

    /// <summary>
    /// Asynchronously loads the achievements data from Cloud Save.
    /// This method attempts to load achievement data based on the achievement list from the AchievementManager.
    /// It returns a dictionary where each key corresponds to an achievement ID, and the value indicates whether the achievement is unlocked (true/false).
    /// If the load operation fails, an error message is logged.
    /// </summary>
    /// <returns>
    /// A dictionary containing the loaded achievements data, with keys as achievement IDs and values as booleans indicating whether each achievement is unlocked.
    /// </returns>
    public async Task<Dictionary<string, bool>> LoadAchievementsData()
    {
        Dictionary<string, bool> loadedAchievements = new Dictionary<string, bool>();

        try
        {
            var savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string>(AchievementManager.instance.AchievementList.Select(a => a.Key)));
            foreach (var key in savedData.Keys)
            {
                loadedAchievements[key] = savedData[key] != null && (savedData[key] as string).ToLower() == "true";
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error retrieving achievements: " + e.Message);
        }

        return loadedAchievements;
    }
}
