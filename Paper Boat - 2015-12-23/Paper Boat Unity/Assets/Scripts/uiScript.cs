
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;// RC

public class uiScript : MonoBehaviour {
    public GameObject[] boats;
    public int[] boatCost;
    public bool[] boatBought;
	int[] boatMultiplier;

    public Vector3 moveTo;
    public Vector3 moveBy;

    public GameObject manager;
    public GameObject bridge;
    public GameObject ui;
    public GameObject gameMenu;

	public GameObject slipButton;

    bool move;

    public int slips;
    public Text fpsText;
	public Text slipText;

    int boatSpawnIndex;

	public int score;
	
	public GameObject PauseMenu;		// RC --- Required to show pause menu screen
	public GameObject InternetMessage;		// RC --- Required to show pause menu screen

	public Text distanceXMultiplier;
	public GameObject SubmitScoreButton;

	string message ="";	// RC --- Required to show message on the screen
	string internetNotconnected = "Internet not connected";
	string blank = "";
	string userNotLoggedIn = "User not Logged In";
	string loggedIn = "Player Logged In";

	IAchievement achievement ;

	int count =1;// RC --- to deduct slips in power of 2. Used in the method useslips()

	int slipsCollectedInOneGame =0;

    void Start()
    {
		/*boat multiplier*/
		boatMultiplier = new int[6];
		boatMultiplier[0] = 1;
		boatMultiplier[1] = 2;
		boatMultiplier[2] = 3;
		boatMultiplier[3] = 4;
		boatMultiplier[4] = 5;
		boatMultiplier[5] = 6;

        
		/*set boats cost*/
        boatCost = new int[6];
        boatCost[0] = 1;
        boatCost[1] = 1;
        boatCost[2] = 1;
        boatCost[3] = 1;
        boatCost[4] = 1;
        boatCost[5] = 1;

		boatBought = new bool[6]; //checks if a boat is bought

		Advertisement.Initialize("1023219", true);//initialize ad
       	
		PlayerPrefs.SetString("ReviveByAds","True");	//--- RC to check that revive button is pressed only once

		/*google play services setup*/
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
		//PlayerPrefs.SetInt("slips", 10000);
        
		/*get slips value from registry*/
		slips = PlayerPrefs.GetInt("slips", 0);
		slipText.text = "Slips : " + slips.ToString ();
        
		/*sets up camera for ui*/
		move = true;
        moveTo = Camera.main.transform.position;

		message = blank;		// RC ----- to hide Internetcheck message

		achievement = Social.CreateAchievement();

		PlayerPrefs.SetString("BoatRevived", "false");
		slipsCollectedInOneGame =0;
		PlayerPrefs.SetInt("slipsCollectedInOneGame", slipsCollectedInOneGame);
    }

    public void showAd()
    {
		if(PlayerPrefs.GetString("ReviveByAds") == "True")		//--- RC to check that revive button is pressed only once
		{
			if (Advertisement.IsReady("video"))				//if (Advertisement.IsReady())
        	{
				Advertisement.Show("video", new ShowOptions {			//RC---
					resultCallback = result => {						//RC---
						Debug.Log(result.ToString());					//RC---
					}													//RC---
				});														//RC---  Previous --> //Advertisement.Show();
				PlayerPrefs.SetString("ReviveByAds","False");				//--- RC to check that revive button is pressed only once
				GameObject.FindGameObjectWithTag("boat").transform.position = new Vector3(2.7f,0.05f,-0.91f);	//RC --- To set the boat to previous locationa nd to resume the game
				gameMenu.transform.FindChild("Revive_Menu").gameObject.SetActive(false);			// RC --- transform.FindChild("GameMenus").gameObject.SetActive(false);
				PlayerPrefs.SetString("gameRunning","true");			//RC --- !!
				PlayerPrefs.SetInt("collisionCount",0); // RC - !!
				PlayerPrefs.SetString("BoatRevived", "true");
				Play();
			}		
        }
    }

	/*checks if the player has enough slips to revive*/
	public void checkSlips(int value)
	{
		if (slips - value < 0)
		{
			slipButton.SetActive(false);
		}
		
	}
	
	/*use Slips to revive*/
	public void useSlips()
	{
		Time.timeScale = 1;
		slips -= 100*count;
		count*=2;
		PlayerPrefs.SetInt("slips", slips);
		slipText.text = "Slips : " + slips.ToString ();
		gameMenu.transform.FindChild("Revive_Menu").gameObject.SetActive(false);					// Now working - RC --- transform.FindChild("GameMenus").gameObject.SetActive(false);//not working 
		GameObject.FindGameObjectWithTag("boat").transform.position = new Vector3(2.7f,0.05f,-0.91f);
		PlayerPrefs.SetString("gameRunning","true");			//RC --- !!
		PlayerPrefs.SetInt("collisionCount",0); // RC - !!
		PlayerPrefs.SetString("BoatRevived", "true");
		Play();
	}


    public void login()
    {
        Social.Active.localUser.Authenticate(returnLogin); // google play login
    }

    void returnLogin(bool success)
    {
        if (success)
        {
			message = loggedIn;
        }
        else
        {
			Debug.Log("Success-------" + success);
			message = internetNotconnected;			// RC ----- Message to display internet connection		
        }
    }

	public void logout()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
		Debug.Log("Logout");
		message = userNotLoggedIn;
	}

    void Update()
    {
		fpsText.text = "Score : " + score.ToString ();//sets score text
		CalculateDistance();
		if (move)
        {
            moveBy = Vector3.MoveTowards(Camera.main.transform.position, moveTo, 1);
            Camera.main.transform.position = moveBy;
        }
		InternetMessage.GetComponent<Text>().text = message;		// RC ----- to hide Internetcheck message
		BackHardwareKeyPressed();							// RC ----- for functionallity of backhardwarekey

    }

	/*starts game*/
    public void playGame()
    {
		Time.timeScale =1; //--- Needed to replay the game --// RC 
		manager.SetActive(true);
		bridge.SetActive(false);
		ui.SetActive(false);
        move = false;
		/*Instantiates the selected boat*/
		PlayerPrefs.SetInt("BoatNumber", boatSpawnIndex);		//RC --- To save the boat number and then to calculate the score.
        Instantiate(boats[boatSpawnIndex], new Vector3(2.7f, 0.05f, -0.91f), Quaternion.identity);
		message = "";		// RC ----- to hide Internetcheck message
    }

	public void showAchievements()// RC ----- to check player is logged in and then only show achievements
    {
		Social.localUser.Authenticate (success => {
			if (success) {
				Social.Active.ShowAchievementsUI();			// Originally only this line was present
			}
			else
				InternetNotAvailable();
				//message = userNotLoggedIn;
		});
    }

	public void showLeaderboard()
	{	
		Social.localUser.Authenticate (success => {
			if (success) {
				Social.Active.ShowLeaderboardUI();			// Originally only this line was present
			}
			else
				InternetNotAvailable();
				//	message = userNotLoggedIn;
		});
	}

	public void submitScoreLeaderboard()
	{	
		Social.localUser.Authenticate (success => {
			if (success)
			{
				Social.ReportScore(score*100, "CgkIgr6bkeMCEAIQDg", (succes) => {							// handle success or failure
				});
				SubmitScoreButton.transform.FindChild("title").GetComponent<Text>().text = "Score subimitted";
			}
			else
			{
				SubmitScoreButton.transform.FindChild("title").GetComponent<Text>().text = "Problem with internet connection";
			}
		});
	}



    public void gotoPanel(float xVal)
    {
        moveTo = new Vector3(xVal, moveTo.y, moveTo.z);/*sets the destinantion of camera in UI*/
    }

	public void gotoPanelBoatMenu(int xVal)
	{
		switch(xVal)
		{
			case 1: if(CheckSlipsValue(1000)){gotoPanel(17.93f);}break;
			case 2: if(CheckSlipsValue(2000)){gotoPanel(17.93f);}break;
			case 3: if(CheckSlipsValue(3000)){gotoPanel(17.93f);}break;
			case 4: if(CheckSlipsValue(4500)){gotoPanel(17.93f);}break;
			case 5: if(CheckSlipsValue(6000)){gotoPanel(17.93f);}break;
			case 6: if(CheckSlipsValue(10000)){gotoPanel(17.93f);}break;
			default : break;
		}
			
	}

	bool CheckSlipsValue(int value)
	{
		slips = PlayerPrefs.GetInt("slips");
		if (slips - value <= 0)
		{
			print("kangaal Manushya");
			ui.gameObject.transform.FindChild("Boat_Menu").gameObject.transform.FindChild("Not Enough Slips").gameObject.SetActive(true);
			return false;
		}
		else
		{
			PlayerPrefs.SetInt("slips", slips - value);
			slipText.text = "Slips : " + slips.ToString ();
			addSlips();
			print("Paise wala");
			HideNotEnoughSlips();
			return true;
		} 
	}

	public void HideNotEnoughSlips()
	{
		ui.gameObject.transform.FindChild("Boat_Menu").gameObject.transform.FindChild("Not Enough Slips").gameObject.SetActive(false);
	}

	/*buy boats and themes*/
    public void buyItem(int value)
    {
        if (slips - value < 0)
        {
            print("kangaal Manushya");
        }
        else
        {
            slips -= value;
            PlayerPrefs.SetInt("slips", slips);
            print("Bahut paise aa gaye hain !!!");
        }
    }


	/*slips collected in game*/
    public void addSlips()
    {
		slips =  PlayerPrefs.GetInt("slips");
        slips++;
        PlayerPrefs.SetInt("slips", slips);
		slipText.text = "Slips : " + slips.ToString ();
		slipsCollectedInOneGame++;
		PlayerPrefs.SetInt("slipsCollectedInOneGame", slipsCollectedInOneGame);
    }

    public void buyBoat(int index)
    {
        if (boatBought[index])
        {
            boatSpawnIndex = index;
        }
        else
        {
            boatSpawnIndex = index;
        }
    }

	//------ Created by RC-------//

	public void CloseGame()		 //--  closes the game 
	{
		Application.Quit();
		Debug.Log("Game closed");
	}
	public void PauseGame()		 //--  pauses the game 
	{
		Time.timeScale =0;
		Debug.Log("Game Paused");
		ShowPauseMenu();
	}
	public void Play()		 //-- resumes the game 
	{
		Time.timeScale =1;
		Debug.Log("Game Resume");
		HidePauseMenu();
	}

	public void ShowPauseMenu()	//-- Show pause menu
	{
		gameMenu.transform.FindChild("Pause_Menu").gameObject.SetActive(true);
	}
	public void HidePauseMenu() //-- Hide pause menu
	{
		gameMenu.transform.FindChild("Pause_Menu").gameObject.SetActive(false);
	}

	public void ReloadScene() //-- Reloads the curret scene. It is used when the game is over
	{
		Application.LoadLevel(""+Application.loadedLevelName);
	}

	public void PauseAndPlay() //-- Hide pause menu
	{
		if(Time.timeScale==0)
		{
			Play();
		}
		else if(Time.timeScale==1)
		{
			PauseGame();
		}
	}

	public void BackHardwareKeyPressed()
	{
		if(Input.GetKeyDown(KeyCode.Escape) 
		   && ui.activeInHierarchy == false 
		   && gameMenu.transform.FindChild("Revive_Menu").gameObject.activeInHierarchy == false 
		    && gameMenu.transform.FindChild("GameOver_Menu").gameObject.activeInHierarchy == false
		   && gameMenu.transform.FindChild("SubmitScore").gameObject.activeInHierarchy == false)
		{
			PauseAndPlay();
			message = blank;
		}
	}

	public void EndBack()
	{
		gameMenu.transform.FindChild("GameOver_Menu").gameObject.SetActive(true);
		gameMenu.transform.FindChild("Revive_Menu").gameObject.SetActive(false);
	}

	void CalculateDistance()			 //RC -- Calculates the Distance
	{
		switch(PlayerPrefs.GetInt("BoatNumber"))
		{
		case 0: distanceXMultiplier.text = score.ToString () + " X 1.2" +  " = " + score*1.2f; break;
		case 1: distanceXMultiplier.text = score.ToString () + " X 1.4" +  " = " + score*1.4f;break;
		case 2: distanceXMultiplier.text = score.ToString () + " X 1.6" +  " = " + score*1.6f;break;
		case 3: distanceXMultiplier.text = score.ToString () + " X 1.8" +  " = " + score*1.8f;break;
		case 4: distanceXMultiplier.text = score.ToString () + " X 2.0" +  " = " + score*2.0f;break;
		case 5: distanceXMultiplier.text = score.ToString () + " X 3.0" +  " = " + score*3.0f;break;
		case 6: distanceXMultiplier.text = score.ToString () + " X 4.0" +  " = " + score*4.0f; break;
		default: distanceXMultiplier.text = score.ToString () + " X 1.2" +  " = " + score*1.2f; break;
		}

	}

	public void SubmitScore()
	{
		gameMenu.transform.FindChild("GameOver_Menu").gameObject.SetActive(false);
		SubmitScoreButton.gameObject.SetActive(true);
	}

	public void BackToSubmitScore()
	{
		SubmitScoreButton.gameObject.SetActive(false);
		gameMenu.transform.FindChild("GameOver_Menu").gameObject.SetActive(true);
	}

	public void InternetNotAvailable()
	{
		ui.transform.FindChild("Popup").gameObject.SetActive(true);
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(false);
	}
	
	public void BackToMainMenu()
	{
		ui.transform.FindChild("Popup").gameObject.SetActive(false);
		ui.transform.FindChild("Main_Menu").gameObject.SetActive(true);

	}

}
