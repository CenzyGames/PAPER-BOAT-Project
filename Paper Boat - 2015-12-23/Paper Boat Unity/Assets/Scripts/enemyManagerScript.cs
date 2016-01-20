using UnityEngine;
using System.Collections;

public class enemyManagerScript : MonoBehaviour
{
    public float time;
    enum obj{petal, duck, fish, island};
    public GameObject[] objects;
    GameObject currentObject;
    public GameObject slip;
    int objNum;

	void Start ()
    {
        StartCoroutine("spawnObj");
        StartCoroutine("spawnSlip");
    }

    IEnumerator spawnSlip()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(slip,slip.transform.position, Quaternion.identity);
        StartCoroutine("spawnSlip");
    }

    IEnumerator spawnObj()
    {
        int num = Random.Range(0, 10);
        if (num % 3 == 0 && num != 0)
        {
            objNum = (int)obj.duck;
        }
        else if (num % 4 == 0 && num != 0)
        {
            objNum = (int)obj.fish;
        }
        else if (num == 7)
        {
            objNum = (int)obj.island;
        }
        else
        {
            objNum = (int)obj.petal;
        }
        currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
        currentObject.transform.parent = transform;
        yield return new WaitForSeconds(time);
        time = Random.Range(2.0f, 4.0f);
        StartCoroutine("spawnObj");
    }
}
