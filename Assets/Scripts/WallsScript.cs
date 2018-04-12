using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsScript : MonoBehaviour {

	public bool touching;
	public GameObject foundation;

	GameObject player;
	FoundationScript1 foundationScript;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		foundationScript = foundation.GetComponent <FoundationScript1> ();
	}

	void Update ()
	{
		if (foundationScript.touching == true) 
		{
			touching = true;
		}

		if (foundationScript.touching == false) 
		{
			touching = false;
		}
	}

	void OnTriggerEnter (Collider other)
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
