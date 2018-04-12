using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour {

	public static int ammo;

	Text text;
	PlayerShooting shooting;
	GameObject player;

	void Awake ()
	{
		text = GetComponent <Text> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		shooting = player.GetComponentInChildren <PlayerShooting> ();

	}

	void Update ()
	{
		text.text = shooting.gunAmmo + "/" + shooting.maxAmmo;
	}
}