using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour {

	void Awake ()
	{
		StartCoroutine (Destroy ());
	}
	IEnumerator Destroy () {
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}

	void  OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag != "Shotgun Bullet") 
		{
			Destroy (gameObject);
		}
	}
}