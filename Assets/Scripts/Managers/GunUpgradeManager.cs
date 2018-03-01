using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgradeManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public GameObject GunUpgrade;
	public float spawnTime = 30f;
	public Transform[] spawnPoints;


	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (GunUpgrade, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
