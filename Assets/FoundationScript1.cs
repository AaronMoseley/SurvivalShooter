using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationScript1 : MonoBehaviour {

	public GameObject roof;
	public GameObject walls;
	public bool touching;

	GameObject player;
	MeshRenderer mesh;
	WallsScript wallsScript;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		mesh = roof.GetComponent <MeshRenderer> ();
		wallsScript = walls.GetComponent <WallsScript> ();
	}

	void Update ()
	{
		if (touching == true || wallsScript.touching == true) 
		{
			mesh.enabled = false;
		} else  if (touching == false || wallsScript.touching == false) 
		{
			mesh.enabled = true;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject == player) 
		{
			touching = true;
		} 
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == player) 
		{
			touching = false;
		}
	}
}
