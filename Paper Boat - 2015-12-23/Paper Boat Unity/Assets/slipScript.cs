using UnityEngine;
using System.Collections;

public class slipScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, 0.05f, Random.Range(0.6f, 1.6f) * -1);
		StartCoroutine ("move");
    }

	IEnumerator move()
	{
		transform.Translate(-0.01f* Time.timeScale, 0, 0) ;
		if (transform.position.x < -6)
		{
			Destroy(gameObject,0.1f);
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("move");
	}
}
