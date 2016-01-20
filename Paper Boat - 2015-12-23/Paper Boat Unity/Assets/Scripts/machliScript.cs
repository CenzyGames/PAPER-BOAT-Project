using UnityEngine;
using System.Collections;

public class machliScript : MonoBehaviour {
    float val;
    float count;
    public float speed;
    public float distance;
    public float position;
    public float angle;

    public GameObject ripple;
    GameObject ripple_clone;
    public float rippleDelay;

    void Start ()
    {
        StartCoroutine("createRipple");
		StartCoroutine ("move");
	}

    IEnumerator createRipple()
    {
        ripple_clone = Instantiate(ripple, transform.position - (Vector3.right*0.25f), Quaternion.identity) as GameObject;
        ripple_clone.GetComponent<rippleScript>().delay = 1.0f;
        ripple_clone.GetComponent<rippleScript>().rippleScale = 0.001f;
        yield return new WaitForSeconds(rippleDelay);
        StartCoroutine("createRipple");
    }

	IEnumerator move()
	{
		angle = Mathf.Sin(count) * 15;
		val = Mathf.Sin(count)*distance;
		transform.eulerAngles = new Vector3(0, 270-angle, 0);
		transform.position = new Vector3(transform.position.x, transform.position.y, position + val);
		transform.position += new Vector3(-0.015f, 0, 0) * Time.timeScale;
		count += speed;
		
		if (transform.position.x < -4.0f)
		{
			Destroy(gameObject);
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine ("move");
	}
}
