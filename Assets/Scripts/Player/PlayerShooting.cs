using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float rifletimeBetweenBullets = 0.15f;
	public float subtimeBetweenBullets = .04f;
	public float shotguntimeBetweenBullets = 1f;
	public float snipertimeBetweenBullets = 3f;
    public float range = 100f;
	public float reloadTime = 1.5f;
	public int gunAmmo = 30;
	public int maxAmmo = 30;
	public float zeroReload = 3f;
	public float reload = 1f;
	public bool rifle;
	public bool sub;
	public bool sniper;
	public bool shotgun;
	public int subDamage = 15;
	public int rifleDamage = 20;
	public int sniperDamage = 700;
	public int shotgunDamage = 10;
	public Rigidbody shotgunBullet;
	public Rigidbody sniperBullet;
	public Rigidbody subBullet;
	public Rigidbody rifleBullet;
	public Transform barrelEnd;
	public int rifleUpgrade = 8;
	public int subUpgrade = 5;
	public int shotgunUpgrade = 8;
	public float sniperUpgrade = 0.08f;
	public int shotCount = 50;
	public float spreadRadius = 0.2f;
	public GameObject subGun;
	public GameObject rifleGun;
	public GameObject shotgunGun;
	public GameObject sniperGun;


    float rifleTimer;
	float subTimer;
	float sniperTimer;
	float shotgunTimer;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
	bool reloading = false;
	GameObject player;


    void Awake ()
    {
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
		subGun = GameObject.FindGameObjectWithTag ("sub");
		shotgunGun = GameObject.FindGameObjectWithTag ("shotgun");
		sniperGun= GameObject.FindGameObjectWithTag ("sniper");
		rifleGun = GameObject.FindGameObjectWithTag ("rifle");
		player = GameObject.FindGameObjectWithTag ("Player");
		rifle = true;
		sub = false;
		sniper = false;
		shotgun = false;
    }

	public void Upgrading ()
	{
		rifleDamage += rifleUpgrade;
		subDamage += subUpgrade;
		shotgunDamage += shotgunUpgrade;
		snipertimeBetweenBullets -= sniperUpgrade;
	}

	void Update ()
	{
		rifleTimer += Time.deltaTime;
		subTimer += Time.deltaTime;
		sniperTimer += Time.deltaTime;
		shotgunTimer += Time.deltaTime;

		if (Input.GetButton ("Fire1") && rifleTimer >= rifletimeBetweenBullets && Time.timeScale != 0 && reloading == false && gunAmmo > 0 && rifle) {
			ShootRifle ();
		}

		if (Input.GetButton ("Fire1") && shotgunTimer >= shotguntimeBetweenBullets && Time.timeScale != 0 && reloading == false && gunAmmo > 0 && shotgun == true) {
			ShootShotgun ();
		}

		if (Input.GetButton ("Fire1") && subTimer >= subtimeBetweenBullets && Time.timeScale != 0 && reloading == false && gunAmmo > 0 && sub == true) {
			ShootSub ();
		}

		if (Input.GetButton ("Fire1") && sniperTimer >= snipertimeBetweenBullets && Time.timeScale != 0 && reloading == false && gunAmmo > 0 && sniper == true) {
			ShootSniper ();
		}

		if (rifleTimer >= rifletimeBetweenBullets * effectsDisplayTime && rifle == true) {
			DisableEffects ();
		}

		if (subTimer >= subtimeBetweenBullets * effectsDisplayTime && sub == true) {
			DisableEffects ();
		}

		if (sniperTimer >= snipertimeBetweenBullets * effectsDisplayTime && sniper == true) {
			DisableEffects ();
		}

		if (shotgunTimer >= shotguntimeBetweenBullets * effectsDisplayTime && shotgun == true) {
			DisableEffects ();
		}
			
		if (gunAmmo == 0) 
		{
			reloadTime = zeroReload;
		}

		if (Input.GetKeyDown (KeyCode.R) || gunAmmo == 0 && !reloading) 
		{
			reloading = true;
			StartCoroutine (Reload());
		}

		if (sniper == true) 
		{
			damagePerShot = sniperDamage;
			maxAmmo = 1;
		}

		if (sub == true) 
		{
			damagePerShot = subDamage;
			maxAmmo = 75;
		}

		if (rifle == true) 
		{
			damagePerShot = rifleDamage;
			maxAmmo = 30;
		}

		if (shotgun == true) 
		{
			damagePerShot = shotgunDamage;
			maxAmmo = 8;
		}

		if (gunAmmo > maxAmmo) 
		{
			gunAmmo = maxAmmo;
		}
	}
		
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void ShootRifle ()
    {
			gunAmmo -= 1;	

			rifleTimer = 0f;

			gunAudio.Play ();

			gunLight.enabled = true;

			gunParticles.Stop ();
			gunParticles.Play ();

		Rigidbody rifleBulletInstance;

		rifleBulletInstance = Instantiate(rifleBullet, barrelEnd.position, Quaternion.identity) as Rigidbody;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		rifleBulletInstance.transform.rotation = q * rifleBulletInstance.transform.rotation;
		rifleBulletInstance.AddForce (barrelEnd.forward * 5000);
    }

	void ShootSub ()
	{
		gunAmmo -= 1;	

		subTimer = 0f;

		gunAudio.Play ();

		gunLight.enabled = true;

		gunParticles.Stop ();
		gunParticles.Play ();

		Rigidbody subBulletInstance;

		subBulletInstance = Instantiate(subBullet, barrelEnd.position, Quaternion.identity) as Rigidbody;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		subBulletInstance.transform.rotation = q * subBulletInstance.transform.rotation;
		subBulletInstance.AddForce (barrelEnd.forward * 5000);
	}

	void ShootShotgun ()
	{
		gunAmmo -= 1;	

		shotgunTimer = 0f;

		gunAudio.Play ();

		gunLight.enabled = true;

		gunParticles.Stop ();
		gunParticles.Play ();

		for (var i = 0; i < shotCount; i++) 
		{
			Rigidbody bulletInstance = Instantiate (shotgunBullet, barrelEnd.position, transform.rotation) as Rigidbody;
			bulletInstance.transform.rotation *= Quaternion.Euler (Random.Range (-spreadRadius, spreadRadius), Random.Range (-spreadRadius, spreadRadius), 0);
			bulletInstance.velocity = bulletInstance.transform.forward * 150;
		}
	}

	void ShootSniper ()
	{
		gunAmmo -= 1;	

		sniperTimer = 0f;

		gunAudio.Play ();

		gunLight.enabled = true;

		gunParticles.Stop ();
		gunParticles.Play ();

		Rigidbody sniperBulletInstance = Instantiate (sniperBullet, barrelEnd.position, Quaternion.identity) as Rigidbody;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		sniperBulletInstance.transform.rotation = q * sniperBulletInstance.transform.rotation;
		sniperBulletInstance.AddForce (barrelEnd.forward * 5000);
	}

	IEnumerator Reload () {
		yield return new WaitForSeconds (reloadTime);
		gunAmmo = maxAmmo;
		reloadTime = reload;
		reloading = false;
	}
}