using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {

	public int HealAmount = 20;

	PlayerHealth playerHealth;
	GameObject player;
	GameObject medkit;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		medkit = GameObject.FindGameObjectWithTag ("Medkit");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
		
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) 
		{
			Heal ();
			Debug.Log ("jssdfdskfj");
		}

		if (other.gameObject == medkit) 
		{
			Destroy (gameObject);
		}
	}

	void Heal ()
	{
		if (playerHealth.currentHealth < 100) 
		{
			playerHealth.Healing (HealAmount);
			Destroy (gameObject);
		}
	}
}