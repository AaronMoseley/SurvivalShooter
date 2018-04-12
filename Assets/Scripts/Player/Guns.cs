using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour {

	GameObject player;
	bool destroy;
	bool holdingF = false;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		holdingF = Input.GetKey(KeyCode.F);
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject == player && holdingF) 
		{
			Destroy (gameObject);
		}
	}
}