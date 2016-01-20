using UnityEngine;
using System.Collections;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class googlePlayLoginScript : MonoBehaviour {
	public string leaderboard;
	void Start ()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            if (success)
            { 
				Application.LoadLevel("GamePlay"); 
			}
            else
            {
                Debug.Log("Failed");
            }
        } );
    }
	
	public void OnLogOut ()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
	}

	public void OnShowLeaderBoard ()
	{
		//        Social.ShowLeaderboardUI (); // Show all leaderboard
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (leaderboard); // Show current (Active) leaderboard
	}

	public void OnAddScoreToLeaderBorad ()
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (100, leaderboard, (bool success) =>
			                    {
				if (success) {
					Debug.Log ("Update Score Success");
					
				} else {
					Debug.Log ("Update Score Fail");
				}
			});
		}
	}

	
	/// <summary>
	/// Current not in use
	///
		/// </summary>
		void AchievementDetails()
	{
		Social.LoadAchievements (achievements => {
			if (achievements.Length > 0) {
				Debug.Log ("Got " + achievements.Length + " achievement instances");
				string myAchievements = "My achievements:\n";
				foreach (IAchievement achievement in achievements)
				{
					myAchievements += "\t" + 
						achievement.id + " " +
							achievement.percentCompleted + " " +
							achievement.completed + " " +
							achievement.lastReportedDate + "\n";
				}
				Debug.Log (myAchievements);
				//	Test.GetComponent<Text>().text = myAchievements + "";
			}
			else
				Debug.Log ("No achievements returned");
		});
	}
	
}
