using UnityEngine;
using System.Collections;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class AchievementsUnlock : MonoBehaviour {

	public GameObject canvas;
	int scr = 0;
	int totalSlips = 0;

	string id1 = "CgkIgr6bkeMCEAIQAA";		//Complete 20m without colliding
	string id2 = "CgkIgr6bkeMCEAIQAQ";		//Complete 40m without colliding
	string id3 = "CgkIgr6bkeMCEAIQAg";		//Complete 60m without colliding
	string id4 = "CgkIgr6bkeMCEAIQAw";		//Cross 50m without revive
	string id5 = "CgkIgr6bkeMCEAIQBA";		//Cross 75m without revive
	string id6 = "CgkIgr6bkeMCEAIQBQ";		//Cross 100m without revive
	string id7 = "CgkIgr6bkeMCEAIQBg";		//Collect 100 slips without reviving
	string id8 = "CgkIgr6bkeMCEAIQBw";		//Collect 500 slips without reviving
	string id9 = "CgkIgr6bkeMCEAIQCA";		//Collect 50 slips without colliding
	string id10 = "CgkIgr6bkeMCEAIQCQ";		//Collect 70 slips without colliding
	string id11 = "CgkIgr6bkeMCEAIQCg";		//Collect 100 slips without colliding
	string id12 = "CgkIgr6bkeMCEAIQCw";		//Collect 200 slips without colliding
	string id13 = "CgkIgr6bkeMCEAIQDA";		//Collect 50 slips without reviving
	string id14 = "CgkIgr6bkeMCEAIQDQ";		//Collect 70 slips without reviving
	string id15 = "";

	bool status  = false; // status whether achievemnt is completed or not
	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("CheckAchivementStatus");//--- RC'
		totalSlips = PlayerPrefs.GetInt("slips");
	}

	IEnumerator CheckAchivementStatus()
	{
		yield return new WaitForSeconds (1);
		scr = canvas.GetComponent<uiScript> ().score;			//---- RC : Score is send to check the achivement unlocked in Achievementunlocked.cs
		switch(scr)
		{
			case 20: AchievementOccured(1); break;
			case 40: AchievementOccured(2);	break;
			case 50: AchievementOccured(4); break;
		 	case 60: AchievementOccured(3); break;
			case 75: AchievementOccured(5); break;
			case 100: AchievementOccured(6); break;
			default: break;
		}

//		switch(PlayerPrefs.GetInt("collisionCount"))
//		{
//			case 15: AchievementOccured(7); break;
//			case 25: AchievementOccured(8);	break;
//			case 40: AchievementOccured(9); break;
//			default: break;
//		}

		switch(PlayerPrefs.GetInt("slipsCollectedInOneGame"))
		{
			case 50: AchievementOccured(10);AchievementOccured(14); break;
			case 70: AchievementOccured(11);AchievementOccured(15);	break;
			case 100: AchievementOccured(7);AchievementOccured(12); break;
			case 200: AchievementOccured(13); break;
			case 500: AchievementOccured(8); break;
			default: break;
		}

		StartCoroutine ("CheckAchivementStatus");
	}
	// Update is called once per frame
	void Update () 
	{

	}

	void AchievementOccured(int number)
	{
		switch(number)
		{
			case 1: Achievement1(); break;
			case 2: Achievement2();break;
			case 3: Achievement3();break;
			case 4: Achievement4(); break;
			case 5: Achievement5();break;
			case 6: Achievement6();break;
			case 7: Achievement7();break;
			case 8: Achievement8();break;
		//	case 9: Achievement9();break;
			case 10: Achievement10();break;
			case 11: Achievement11();break;
			case 12: Achievement12();break;
			case 13: Achievement13();break;
			case 14: Achievement14();break;
			case 15: Achievement15();break;
			default: break;
		}
	}

	void SubmitProgress(string ID)
	{
		if (Social.localUser.authenticated) {
			Social.ReportProgress (ID, 100.0f, result => {
				if (result)
					Debug.Log ("Successfully reported achievement progress");
				else
					Debug.Log ("Failed to report achievement");
			});
		}
		else
		{
			// SHow connect to internet popup
		}
	}

	bool IsAchievementAlreadyCompleted(string id)
	{
		if (Social.localUser.authenticated)
		{
			Social.LoadAchievements (achievements => {
				if (achievements.Length > 0) 
				{
					foreach (IAchievement achievement in achievements)
					{
						Debug.Log("Achievemnet is completed - " +  achievement.completed );
						if(id == achievement.id && achievement.completed == true)
						{
							status = achievement.completed;
							break;
						}
					}
				}
				else
					Debug.Log ("No achievements returned");
			});
		}
		else
		{
			// SHow connect to internet popup
		}

		return status;

	}

	void addSlips(int slips)
	{
		Debug.Log("Achievement occured");
		totalSlips = PlayerPrefs.GetInt("slips");
		totalSlips+=slips;
		PlayerPrefs.SetInt("slips", totalSlips);
	}

	void Achievement1()		//Complete 20m without colliding
	{
		if(PlayerPrefs.GetString("isCollided")== "False" && !IsAchievementAlreadyCompleted(id1))	
		{	
			addSlips(50);											// Add 50 slips on completion of first achievement
			SubmitProgress(id1);					 
		}
		else
		{

		}
	}
	void Achievement2()	//Complete 40m without colliding
	{
		if(PlayerPrefs.GetString("isCollided")== "False" && !IsAchievementAlreadyCompleted(id2))	
		{	
			addSlips(150);											// Add 50 slips on completion of first achievement
			SubmitProgress(id2);					 
		}
		else
		{
			
		}
	}
	void Achievement3()		//Complete 60m without colliding
	{
		  
		if(PlayerPrefs.GetString("isCollided")== "false" && !IsAchievementAlreadyCompleted(id3))	
		{	
			addSlips(250);											// Add 50 slips on completion of first achievement
			SubmitProgress(id3);					 
		}
		else
		{

		}
	}
	void Achievement4()		//Cross 50m without revive
	{
		if(PlayerPrefs.GetString("BoatRevived")== "false" && !IsAchievementAlreadyCompleted(id4))	
		{	
			addSlips(100);											// Add 50 slips on completion of first achievement
			SubmitProgress(id4);					 
		}
		else
		{
			
		}
	}
	void Achievement5()		// Cross 75m without revive
	{
		if(PlayerPrefs.GetString("BoatRevived")== "false" && !IsAchievementAlreadyCompleted(id5))	
		{	
			addSlips(200);											// Add 50 slips on completion of first achievement
			SubmitProgress(id5);					 
		}
		else
		{
			
		}
	}
	void Achievement6()		// Cross 100m without revive
	{  
		if(PlayerPrefs.GetString("BoatRevived")== "false" && !IsAchievementAlreadyCompleted(id6))	
		{	
			addSlips(400);											// Add 400 slips on completion of first achievement
			SubmitProgress(id6);					 
		}
		else
		{
			
		}
	}
	void Achievement7()		//Collect 100 slips without reviving
	{
		if(PlayerPrefs.GetString("BoatRevived")== "false"  && !IsAchievementAlreadyCompleted(id14))
		{	
			addSlips(300);											// Add 150 slips on completion of first achievement
			SubmitProgress(id14);					 
		}
		else
		{
			
		}
	
	}
	void Achievement8()		//Collect 500 slips without reviving
	{
		if(PlayerPrefs.GetString("BoatRevived")== "false"  && !IsAchievementAlreadyCompleted(id14))
	{	
			addSlips(500);											// Add 150 slips on completion of first achievement
			SubmitProgress(id14);					 
		}
		else
		{
			
		}
	}
//	void Achievement9()		//Collide with 40 objects without revive
//	{  
//		if(PlayerPrefs.GetString("BoatRevived")== "false"  && !IsAchievementAlreadyCompleted(id9))
//			{	
//				addSlips(200);											
//				SubmitProgress(id9);					 
//			}
//			else
//			{
//				
//			}
//	}
	void Achievement10()	//Collect 50 slips without colliding
	{	  
		if(PlayerPrefs.GetString("isCollided")== "false" && !IsAchievementAlreadyCompleted(id10))	
		{	
			addSlips(200);											
			SubmitProgress(id10);					 
		}
		else
		{
			
		}
	}
	void Achievement11()	//Collect 70 slips without colliding
	{  
		if(PlayerPrefs.GetString("isCollided")== "false" && !IsAchievementAlreadyCompleted(id11))	
		{	
			addSlips(350);										
			SubmitProgress(id11);					 
		}
		else
		{
			
		}
		
	}
	void Achievement12()	//Collect 100 slips without colliding
	{  
		if(PlayerPrefs.GetString("isCollided")== "false" && !IsAchievementAlreadyCompleted(id12))	
		{	
			addSlips(600);											
			SubmitProgress(id12);					 
		}
		else
		{
			
		}
		
	}
	void Achievement13()	//Collect 200 slips without colliding
	{  
		if(PlayerPrefs.GetString("isCollided")== "false" && !IsAchievementAlreadyCompleted(id13))	
		{	
			addSlips(120);											
			SubmitProgress(id13);					 
		}
		else
		{
			
		}
	}
	void Achievement14()	//Collect 50 slips without reviving
	{	  
		if(PlayerPrefs.GetString("BoatRevived")== "false"  && !IsAchievementAlreadyCompleted(id14))
		{	
			addSlips(150);											// Add 150 slips on completion of first achievement
			SubmitProgress(id14);					 
		}
		else
		{
			
		}
	}
	void Achievement15()	//Collect 70 slips without reviving
	{  
		if(PlayerPrefs.GetString("BoatRevived")== "false"  && !IsAchievementAlreadyCompleted(id15))
		{	
			addSlips(200);											// Add 150 slips on completion of first achievement
			SubmitProgress(id15);					 
		}
		else
		{
			
		}
		
	}
}
