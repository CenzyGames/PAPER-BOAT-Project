using UnityEngine;
using System;
using System.Collections;

public class RRInappBillingPluginKit  : IDisposable {

	public static bool InitInAppBillingSupport( )
	{		
			AndroidJavaClass inappBilling = new AndroidJavaClass( BillingCONST.PLUGIN_PKG + ".InAppBillingManager" );
			bool reponse = inappBilling.CallStatic<bool>("IsBillingSupported");
			return reponse;
	}	

	public static bool BuyProduct( string productId ){
			AndroidJavaClass inappBilling = new AndroidJavaClass( BillingCONST.PLUGIN_PKG + ".InAppBillingManager" );
			bool reponse = inappBilling.CallStatic<bool>("BuyItem", productId );
			return reponse;			
	} 

    void IDisposable.Dispose(){
    }	

}
