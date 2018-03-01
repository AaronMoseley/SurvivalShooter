using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float speed = 5.5f;

	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	UnityEngine.AI.NavMeshAgent nav;
	Vector3 movement;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = speed;
	}

	void Update ()
	{

		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
			
		{
			nav.SetDestination (player.position);
		}
		else
		{
		    nav.enabled = false;
		}
	}
}
