using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatHighlight : MonoBehaviour {

	public GameObject Boat1Selected;
	public GameObject Boat2Selected;
	public GameObject Boat3Selected;
	public GameObject Boat4Selected;
	public GameObject Boat5Selected;
	public GameObject Boat6Selected;
	public GameObject Yes1Selected;
	public GameObject Yes2Selected;
	public GameObject Yes3Selected;
	public GameObject Yes4Selected;
	public GameObject Yes5Selected;
	public GameObject Yes6Selected;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void SelectedBoat1()
	{
		Boat1Selected.SetActive(true);
		Boat2Selected.SetActive(false);
		Boat3Selected.SetActive(false);
		Boat4Selected.SetActive(false);
		Boat5Selected.SetActive(false);
		Boat6Selected.SetActive(false);
		Yes1Selected.SetActive(true);
		Yes2Selected.SetActive(false);
		Yes3Selected.SetActive(false);
		Yes4Selected.SetActive(false);
		Yes5Selected.SetActive(false);
		Yes6Selected.SetActive(false);
	}
	
	public void SelectedBoat2()
	{
		Boat1Selected.SetActive(false);
		Boat2Selected.SetActive(true);
		Boat3Selected.SetActive(false);
		Boat4Selected.SetActive(false);
		Boat5Selected.SetActive(false);
		Boat6Selected.SetActive(false);
		Yes1Selected.SetActive(false);
		Yes2Selected.SetActive(true);
		Yes3Selected.SetActive(false);
		Yes4Selected.SetActive(false);
		Yes5Selected.SetActive(false);
		Yes6Selected.SetActive(false);
	}
	
	public void SelectedBoat3()
	{
		Boat1Selected.SetActive(false);
		Boat2Selected.SetActive(false);
		Boat3Selected.SetActive(true);
		Boat4Selected.SetActive(false);
		Boat5Selected.SetActive(false);
		Boat6Selected.SetActive(false);
		Yes1Selected.SetActive(false);
		Yes2Selected.SetActive(false);
		Yes3Selected.SetActive(true);
		Yes4Selected.SetActive(false);
		Yes5Selected.SetActive(false);
		Yes6Selected.SetActive(false);
	}
	
	public void SelectedBoat4()
	{
		Boat1Selected.SetActive(false);
		Boat2Selected.SetActive(false);
		Boat3Selected.SetActive(false);
		Boat4Selected.SetActive(true);
		Boat5Selected.SetActive(false);
		Boat6Selected.SetActive(false);
		Yes1Selected.SetActive(false);
		Yes2Selected.SetActive(false);
		Yes3Selected.SetActive(false);
		Yes4Selected.SetActive(true);
		Yes5Selected.SetActive(false);
		Yes6Selected.SetActive(false);
	}
	
	public void SelectedBoat5()
	{
		Boat1Selected.SetActive(false);
		Boat2Selected.SetActive(false);
		Boat3Selected.SetActive(false);
		Boat4Selected.SetActive(false);
		Boat5Selected.SetActive(true);
		Boat6Selected.SetActive(false);
		Yes1Selected.SetActive(false);
		Yes2Selected.SetActive(false);
		Yes3Selected.SetActive(false);
		Yes4Selected.SetActive(false);
		Yes5Selected.SetActive(true);
		Yes6Selected.SetActive(false);
	}
	
	public void SelectedBoat6()
	{
		Boat1Selected.SetActive(false);
		Boat2Selected.SetActive(false);
		Boat3Selected.SetActive(false);
		Boat4Selected.SetActive(false);
		Boat5Selected.SetActive(false);
		Boat6Selected.SetActive(true);
		Yes1Selected.SetActive(false);
		Yes2Selected.SetActive(false);
		Yes3Selected.SetActive(false);
		Yes4Selected.SetActive(false);
		Yes5Selected.SetActive(false);
		Yes6Selected.SetActive(true);
	}

}
