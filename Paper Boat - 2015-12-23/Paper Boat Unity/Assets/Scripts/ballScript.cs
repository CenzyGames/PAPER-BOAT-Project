using UnityEngine;
using System.Collections;

public class ballScript : MonoBehaviour {
    void Update()
    {
        GetComponent<Rigidbody>().mass -= 0.01f;
    }
    void OnCollisionEnter(Collision pCol)
    { 
        if (pCol.gameObject.name == "boat")
        {
            Destroy(gameObject);
        }
    }
}
