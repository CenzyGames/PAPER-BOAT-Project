using UnityEngine;
using System.Collections;

public class SampleInApp : MonoBehaviour {

	/***
	* Consider you have 3 bundle that user can purchase.
	* bundle1: pts_100
	* bundle2: pts_200
	* bundle3: pts_300
	* if user press button for pts_100, then it will show that in the In app billing UI
	* Follow the example to check how to do it.
	*/

	private int userPTS = 0;

	public void Start (){
		//IMPORTANT: Need to call it before you want to use the in app
		RRInappBillingPluginKit.InitInAppBillingSupport ();
	}
	
	void OnEnable(){
		RRInappBillingCallback.purchaseSuccessful 	+= purchaseSuccessful;
		RRInappBillingCallback.purchaseFailed 		+= purchaseFailed;
		RRInappBillingCallback.purchaseCancelled 	+= purchaseCancelled;		
	}
	
	void OnDisable (){
		RRInappBillingCallback.purchaseSuccessful 	-= purchaseSuccessful;
		RRInappBillingCallback.purchaseFailed 		-= purchaseFailed;
		RRInappBillingCallback.purchaseCancelled 	-= purchaseCancelled;		
	}

	void purchaseSuccessful( string productIdentifier )
	{
		Debug.Log( "purchased product: " + productIdentifier );
		
		//Here we are adding point to the user profile PTS that user just purchased
		//For Bundle 1
		if ( productIdentifier == "pts_100" ){
			userPTS += 100;	
			return;
		}
		//For Bundle 2
		if ( productIdentifier == "pts_200" ){
			userPTS += 200; 	
			return;
		}
		//For Bundle 3
		if ( productIdentifier == "pts_300" ){
			userPTS += 300;
			return;
		}
	}	

	void purchaseFailed( string error )
	{
		Debug.Log( "purchaseFailed: " + error );
	}	

	void purchaseCancelled( string error )
	{
		Debug.Log( "purchaseCancelled: " + error );
	}	
	
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 20.0f;
		float width = 210.0f;
		float height = 40.0f;
		float heightPlus = 45.0f;
		
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Buy 100 PTS" ) )
		{
			RRInappBillingPluginKit.BuyProduct ( "pts_100" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Buy 200 PTS" ) )
		{
			RRInappBillingPluginKit.BuyProduct ( "pts_200" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Buy 300 PTS" ) )
		{
			RRInappBillingPluginKit.BuyProduct ( "pts_300" );
		}

	}
	

}
