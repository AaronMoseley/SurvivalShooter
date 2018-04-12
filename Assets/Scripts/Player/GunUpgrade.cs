using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgrade : MonoBehaviour {

	public int upgradeAmount = 7;

	PlayerShooting playerShooting; 
	GameObject GunBarrelEnd;
	GameObject GunUpgrader; 
	GameObject player;

	void Awake ()
	{
		GunBarrelEnd = GameObject.FindGameObjectWithTag ("GunBarrelEnd");
		GunUpgrader = GameObject.FindGameObjectWithTag ("Gun Upgrade");
		player = GameObject.FindGameObjectWithTag ("Player");
		playerShooting = GunBarrelEnd.GetComponent <PlayerShooting> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) 
		{
			Destroy (gameObject);
			playerShooting.Upgrading ();
		} else if (other.gameObject == GunUpgrader) 
		{
			Destroy (gameObject);
		}
	}
}
