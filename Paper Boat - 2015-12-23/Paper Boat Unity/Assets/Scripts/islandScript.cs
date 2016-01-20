using UnityEngine;
using System.Collections;

public class islandScript : MonoBehaviour
{
	void Start ()
    {
        transform.position = new Vector3(transform.position.x, 0.05f, Random.Range(0.8f, 1.2f) * -1);
		StartCoroutine ("move");
    }

	IEnumerator move()
	{
		transform.Translate(-0.01f, 0, 0) ;
		if (transform.position.x < -4)
		{
			Destroy(gameObject,0.1f);
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("move");
	}
}
