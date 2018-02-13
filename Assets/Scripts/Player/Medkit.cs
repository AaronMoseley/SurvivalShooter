using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {

	PlayerHealth playerHealth;
	GameObject player;
	bool playerTouching;
	public int HealAmount = 20;


	void Awake ()
	{
		Medkit = GameObject.FindGameObjectWithTag ("medkit");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player)
		{
			playerTouching = true;
		}
	}

	void Update () {
		if (playerTouching && playerHealth.currentHealth < 120) 
		{
			Heal ();
		}
	}

	void Heal ()
	{
		if (playerHealth.currentHealth < 120) 
		{
			playerHealth.Heal (HealAmount);
		}
	}
}
