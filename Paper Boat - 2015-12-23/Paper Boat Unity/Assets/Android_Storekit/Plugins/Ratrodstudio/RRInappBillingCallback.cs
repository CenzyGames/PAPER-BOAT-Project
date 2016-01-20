using UnityEngine;
using System;
using System.Collections;

public class RRInappBillingCallback : MonoBehaviour {

	// Events and delegates
	public delegate void ProductPurchasedEventHandler( string productIdentifier );	
	public delegate void StoreKitErrorEventHandler( string error );

	// Fired when a product purchase fails
	public static event StoreKitErrorEventHandler purchaseFailed;
	
	// Fired when a product purchase is cancelled by the user or system
	public static event StoreKitErrorEventHandler purchaseCancelled;

	// Fired when a product is successfully paid for. 
	public static event ProductPurchasedEventHandler purchaseSuccessful;

	void Awake (){
		// Set the GameObject name to the class name for easy access from Java
		gameObject.name = this.GetType().ToString();
		DontDestroyOnLoad ( gameObject );		
	}

	public void productPurchased( string productIdentifier )
	{
		if( purchaseSuccessful != null )
			purchaseSuccessful( productIdentifier );
	}
	
	
	public void productPurchaseFailed( string error )
	{
		if( purchaseFailed != null )
			purchaseFailed( error );
	}
	
		
	public void productPurchaseCancelled( string error )
	{
		if( purchaseCancelled != null )
			purchaseCancelled( error );
	}

}
