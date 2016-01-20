using UnityEngine;
using System.Collections;

public class boatScript : MonoBehaviour
{
	public Vector3 vel;
    float newVelX;
    float newVelZ;

    float xPos;
    float moveX;

    Vector3 cameraPos;
    float difference;
    float camXPos;
    float xDiff;

    public GameObject canvas;

	int reviveCount;

	static bool isCollided = false; // RC --- to check whether boat has been collided or not
	bool gameRunning = true;

    void Start()
    {
		isCollided = false; /// RC --- to set to false ion start
		PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to false ion start
		reviveCount = 1;
		canvas = GameObject.Find("Canvas");
        xPos = transform.position.x;
        cameraPos = Camera.main.transform.position;
        difference = cameraPos.z - transform.position.z;
        camXPos = transform.position.z + difference;
		StartCoroutine ("scoreAdd");
		PlayerPrefs.SetString("gameRunning", "true");		//RC --- !!
    }

	/*increase score with time*/
	IEnumerator scoreAdd()
	{
		yield return new WaitForSeconds (1);
		canvas.GetComponent<uiScript> ().score++;
		StartCoroutine ("scoreAdd");
	}

    void Update ()
    {
		if(PlayerPrefs.GetString("gameRunning")== "true")			// RC ---!! to activate use slips option
		{
			/*resets the camera angle to 0 after its rotated*/
			float angle = Mathf.LerpAngle (transform.eulerAngles.y, 0, Time.deltaTime);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, angle, transform.eulerAngles.z);
			
			/*resets boats's X position*/
       		 if (xPos - transform.position.x >= 0)
       	 	{
        		    moveX = Mathf.Lerp(transform.position.x, xPos, Time.deltaTime * (xPos - transform.position.x));
       		 }
      		  else
      	 	 {
       	 	    moveX = Mathf.Lerp(transform.position.x, xPos, Time.deltaTime);
       		 }
       	 	transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
       		 camXPos = Mathf.Lerp(Camera.main.transform.position.z, transform.position.z + difference, Time.deltaTime*2);
       		 Camera.main.transform.position = new Vector3(cameraPos.x,cameraPos.y,camXPos);

			/*resets boat's velocity*/
			vel = GetComponent<Rigidbody> ().velocity;
      	  	newVelX = Mathf.Lerp(vel.x, 0, Time.deltaTime);
       	 	newVelZ = Mathf.Lerp (vel.z, 0, Time.deltaTime);
			GetComponent<Rigidbody> ().velocity = new Vector3(newVelX,0,newVelZ);
			/*game over condition*/
			//if (!GetComponent<Renderer>().isVisible && transform.position.x < 1.5f) ----------------RC changed the condition as  Renderer value was coming to false and was causing problem in game over
			if (transform.position.x < 1.4f || PlayerPrefs.GetInt("collisionCount")==3)
     	   {
				canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").gameObject.SetActive(true);				// RC ---- canvas.transform.FindChild("GameMenus").gameObject.SetActive(true);
				canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").transform.FindChild("Slips").gameObject.SetActive(true);
				if(PlayerPrefs.GetString("ReviveByAds") == "False")
				{
					canvas.transform.FindChild("GameMenus").transform.FindChild("Revive_Menu").FindChild("showAd").gameObject.SetActive(false);
				}
				canvas.GetComponent<uiScript>().checkSlips(reviveCount*100);//slips deducted
				reviveCount *= 2;
				PlayerPrefs.SetString("gameRunning","false");		//RC --- !!
				Time.timeScale = 0;
      	  }
		
		}
	}


    void OnTriggerEnter(Collider pCol)
    {
        if (pCol.gameObject.tag == "slip")
        {
			pCol.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			pCol.gameObject.GetComponent<BoxCollider>().enabled = false;
			pCol.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			Destroy(pCol.gameObject,0.5f);
            canvas.GetComponent<uiScript>().addSlips();
        }

		if(pCol.gameObject.tag == "duck")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "fish")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "lillypad")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
		if(pCol.gameObject.tag == "island")
		{
			isCollided = true;
			PlayerPrefs.SetString("isCollided",isCollided+"");	/// RC --- to set to true 
		}
    }


}
