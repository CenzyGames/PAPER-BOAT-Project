using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    Vector3 pos;

    void Start ()
    {
        pos = transform.position;
	}
	
	void Update ()
    {
        Vector3 moveTo = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, pos.z), 0.01f);
        transform.position = new Vector3(transform.position.x, transform.position.y, moveTo.z);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                print(hit.point);
                pos = hit.point;
            }
        }
        
	}
}
