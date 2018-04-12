using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	PlayerShooting shooting;

	GameObject player;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
	GameObject water;
	//GameObject rifleBullet;
	//GameObject shotgunBullet;
	//GameObject subBullet;
	//GameObject sniperBullet;


    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
		water = GameObject.FindGameObjectWithTag ("Water");
		shooting = player.GetComponentInChildren <PlayerShooting> ();
		//rifleBullet = GameObject.FindGameObjectWithTag ("Rifle Bullet");
		//shotgunBullet = GameObject.FindGameObjectWithTag ("Shotgun Bullet");
		//subBullet = GameObject.FindGameObjectWithTag ("Sub Bullet");
		//shotgunBullet = GameObject.FindGameObjectWithTag ("Shotgun Bullet");
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == water) {
			gameObject.GetComponent<Collider> ().enabled = false;
			Destroy (gameObject);
		}

		if (other.gameObject.tag == "Rifle Bullet") 
		{
			takeRifleDamage ();
		}

		if (other.gameObject.tag == "Sub Bullet") 
		{
			takeSubDamage ();
		}

		if (other.gameObject.tag == "Sniper Bullet") 
		{
			takeSniperDamage ();
		}

		if (other.gameObject.tag == "Shotgun Bullet") 
		{
			takeShotgunDamage ();
		}
	}


    public void takeRifleDamage ()
    {
        if(isDead)
            return;

        enemyAudio.Play ();

		currentHealth -= shooting.rifleDamage;
            
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }

	public void takeSubDamage ()
	{
		if(isDead)
			return;

		enemyAudio.Play ();

		currentHealth -= shooting.subDamage;

		hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	public void takeSniperDamage ()
	{
		if(isDead)
			return;

		enemyAudio.Play ();

		currentHealth -= shooting.sniperDamage;

		hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	public void takeShotgunDamage ()
	{
		if(isDead)
			return;

		enemyAudio.Play ();

		currentHealth -= shooting.shotgunDamage;

		hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
		}
	}


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
