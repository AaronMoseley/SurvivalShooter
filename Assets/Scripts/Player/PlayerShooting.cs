using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
	public float reloadTime = 1.5f;
	public int gunAmmo = 30;
	public int ammo = 30;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
	float reloadTimer = 0f;
	bool reloading;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

	public void Upgrading (int amount)
	{
		damagePerShot += amount;
	}

	void Update ()
	{
		timer += Time.deltaTime;
		AmmoManager.ammo = gunAmmo;

		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && reloading == false && gunAmmo > 0) {
			Shoot ();
		}

		if (timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			reloading = true;
			StartCoroutine (Reload());
		}

		if (gunAmmo == 0) {
			reloadTime = 3f;
			reloading = true;
			StartCoroutine (Reload());
		}
	}
		
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
			gunAmmo -= 1;	

			timer = 0f;

			gunAudio.Play ();

			gunLight.enabled = true;

			gunParticles.Stop ();
			gunParticles.Play ();

			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position);

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

			if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
				EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
				if (enemyHealth != null) {
					enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				}
				gunLine.SetPosition (1, shootHit.point);
			} else {
				gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
			}
    }

	IEnumerator Reload () {

		if (reloading == false) 
		{
			yield return new WaitForSeconds (reloadTime);
			gunAmmo = ammo;
			reloadTime = 1f;
			print ("DONE");
			reloading = false;
		}
	}
}