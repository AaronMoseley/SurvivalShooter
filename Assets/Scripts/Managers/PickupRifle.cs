using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupRifle : MonoBehaviour {

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
		if (movement.pickupRifle == true && shooting.rifle == false) {
			text.enabled = true;
		} else {
			text.enabled = false;
		}
	}
}