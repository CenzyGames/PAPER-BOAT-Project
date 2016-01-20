using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelManagerScript : MonoBehaviour {
    public GameObject[] levels;
    GameObject level;
    public GameObject[] poolObj;
    List<GameObject> activeSets;
    int i;
    float pos;
    public int randomNum;
    int spawnCount;

    string[] names;
    int plainCount;

	void Start ()
    {
        activeSets = new List<GameObject>();
        poolObj = new GameObject[5];
        names = new string[3];
        pos = 20;

		/*Instantiates levels at the beginning*/
        for (i = 0; i < 5; i++)
        {
            if (i > 2)
            {
                InstantiateLevel(pos, 0);
            }
            else
            {
                InstantiateLevel(pos, i);
                names[i] = level.name;
            }
            poolObj[i] = level;
        }

        setLevels();//sets the initial levels

        /*move the sets*/
        StartCoroutine("moveLevel");
    }

    IEnumerator moveLevel()
    {
		activeSets[0].gameObject.transform.Translate(-0.01f,0,0);
		activeSets[1].gameObject.transform.Translate(-0.01f,0,0);
		activeSets[2].gameObject.transform.Translate(-0.01f,0,0);

        if (activeSets[0].gameObject.transform.position.x < -4)
        {
			/*pools new level*/
        	activeSets[0].gameObject.SetActive(false);
            activeSets[0].gameObject.transform.position = new Vector3(20, 0, 0);
            string s = activeSets[0].gameObject.name;
            activeSets.Remove(activeSets[0]);
            spawnNewLevel(s);
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine("moveLevel");
    }

    public void spawnNewLevel(string name)
    {
        randomNum = Random.Range(0, 10);
        string objName = names[0];
        spawnCount++;

        if (spawnCount > 2)
        {
            if (randomNum %3 == 0 && randomNum !=0)
            {
                objName = names[1];
            }
            else if (randomNum == 7 || randomNum == 0)
            {
                objName = names[2];
            }
            spawnCount = 0;
        }
        createNewLevel(objName);
    }

    void InstantiateLevel(float position, int index)
    {
        level = Instantiate(levels[index], transform.position, Quaternion.identity) as GameObject;
        level.transform.parent = transform;
        level.transform.position = new Vector3(position, 0, 0);
        level.SetActive(false);
    }

    void setLevels()
    {
        pos = 0;
        poolObj[2].SetActive(true);
        poolObj[2].transform.position = new Vector3(pos, 0, 0);
        pos += 4;
        activeSets.Add(poolObj[2]);
        for (i = 0; i < 4; i++)
        {
            if (poolObj[i].name == names[0])
            {
                poolObj[i].SetActive(true);
                poolObj[i].transform.position = new Vector3(pos, 0, 0);
                pos += 4;
                activeSets.Add(poolObj[i]);
            }
        }        
    }

    void createNewLevel(string name)
    {
        foreach (GameObject gj in poolObj)
        {
            if (gj.name == name)
            {
                if (!gj.activeSelf)
                {
                   // print(gj.name);
                    gj.SetActive(true);
                    gj.transform.position = new Vector3(4 * (levels.Length - 1), 0, 0);
                    activeSets.Add(gj);
                    break;
                }
            }     
        }
    }
}
