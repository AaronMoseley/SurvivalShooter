using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupSub : MonoBehaviour {

	PlayerMovement movement;
	GameObject player;
	Text text;
	GameObject barrelEnd;
	PlayerShooting shooting;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		movement = player.GetComponent <PlayerMovement> ();
		text = GetComponent<Text> ();
		barrelEnd = GameObject.FindGameObjectWithTag ("GunBarrelEnd");
		shooting = barrelEnd.GetComponent <PlayerShooting> ();
	}

	void Update ()
	{
		if (movement.pickupSub == true && shooting.sub == false) {
			text.enabled = true;
		} else {
			text.enabled = false;
		}
	}
}