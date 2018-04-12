using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	void Awake ()
	{
		StartCoroutine (Destroy ());
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject) 
		{
			Destroy (gameObject);
		}
	}

	IEnumerator Destroy () {
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
}
