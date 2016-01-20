using UnityEngine;
using System.Collections;

public class rippleScript : MonoBehaviour {

    public float delay;
    public float rippleScale;
    int i, j;
    public Vector3 move;
	void Start ()
    {
        	StartCoroutine("activateObj");
        	StartCoroutine("fade");
	}

    IEnumerator activateObj()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator fade()
    {
        yield return new WaitForSeconds(0.1f);
        for (j = 0; j < transform.GetChild(0).childCount; j++)
        {
            transform.GetChild(0).gameObject.transform.GetChild(j).GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, (1/delay)/10);
        }
        StartCoroutine("fade");
    }

		void Update ()
	    {
			 transform.position += move * Time.timeScale;
	       	 for (i = 0; i < transform.childCount; i++)
	       	 {
	           	 if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).gameObject.name != "ball")
	           	 {
	            	    transform.GetChild(i).transform.localScale += Vector3.one * rippleScale * Time.timeScale;
	           	 }
	        }

		}
}
