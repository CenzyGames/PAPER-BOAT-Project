using UnityEngine;
using System.Collections;

public class BoatCollision : MonoBehaviour {
	int collisionCount =0;
	// Use this for initialization
	void Start () 
	{
		collisionCount =0;
		PlayerPrefs.SetInt("collisionCount", collisionCount);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnCollisionEnter(Collision col)
	{
		collisionCount = PlayerPrefs.GetInt("collisionCount");
		switch(col.gameObject.tag)
		{
			case "duck" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount); BoatSink(); break;
			case "fish" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount);BoatSink(); break;
			case "lillypad" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount);BoatSink(); break;
			case "island" : collisionCount+=1; PlayerPrefs.SetInt("collisionCount", collisionCount);BoatSink(); break;
			default : break;
		}

	}

	void BoatSink()
	{
		collisionCount = PlayerPrefs.GetInt("collisionCount");
		switch(collisionCount)
		{
			case 1: transform.position = new Vector3(transform.position.x, -0.009f, transform.position.z);break;
			case 2: transform.position = new Vector3(transform.position.x, -0.012f, transform.position.z); break;
			case 3: transform.position = new Vector3(transform.position.x, -0.019f, transform.position.z);break;
			default: transform.position = new Vector3(transform.position.x, -0.019f, transform.position.z);break;
		}
	}
	
}
