using UnityEngine;
using System.Collections;

public class InAppBillingGUIScript : MonoBehaviour {

	public void Start (){
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
		
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Test In App Success" ) )
		{
			RRInappBillingPluginKit.BuyProduct ( "android.test.purchased" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Test In App Cancel" ) )
		{
			RRInappBillingPluginKit.BuyProduct ( "android.test.canceled" );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Test In App Failed" ) )
		{
			RRInappBillingPluginKit.BuyProduct ( "android.test.item_unavailable" );
		}

	}
	
}
