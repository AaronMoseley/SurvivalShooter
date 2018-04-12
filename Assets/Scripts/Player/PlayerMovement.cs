using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float sprintSpeed = 12f;
	public Vector3 playerToMouse;

	Vector3 movement;                   
	Animator anim;                      
	Rigidbody playerRigidbody;          
	int floorMask;                      
	float camRayLength = 100f;     
	bool sprint;
	PlayerShooting shooting;
	bool holdingF;
	public bool pickupSub;
	public bool pickupRifle;
	public bool pickupShotgun;
	public bool pickupSniper;


	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		shooting = GetComponentInChildren <PlayerShooting> ();
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}


	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);

		Turning ();

		Animating (h, v);

		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			speed = sprintSpeed;
		}

		if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			speed = 6f;
		}

		holdingF = Input.GetKey(KeyCode.F);
	}

	void Move (float h, float v)
	{
		movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;

			//playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;

		anim.SetBool ("IsWalking", walking);
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject == shooting.subGun && holdingF) 
		{
			shooting.sub = true;
			shooting.rifle = false;
			shooting.sniper = false;
			shooting.shotgun = false;
			pickupSub = false;
			pickupSniper = false;
			pickupShotgun = false;
			pickupRifle = false;
		}

		else if (other.gameObject == shooting.rifleGun && holdingF) 
		{
			shooting.rifle = true;
			shooting.sniper = false;
			shooting.sub = false;
			shooting.shotgun = false;
			pickupSub = false;
			pickupSniper = false;
			pickupShotgun = false;
			pickupRifle = false;
		}

		else if (other.gameObject == shooting.sniperGun && holdingF)  
		{
			shooting.sniper = true;
			shooting.rifle = false;
			shooting.shotgun = false;
			shooting.sub = false;
			pickupSub = false;
			pickupSniper = false;
			pickupShotgun = false;
			pickupRifle = false;
		}

		else if (other.gameObject == shooting.shotgunGun && holdingF) 
		{
			shooting.shotgun = true;
			shooting.rifle = false;
			shooting.sub = false;
			shooting.sniper = false;
			pickupSub = false;
			pickupSniper = false;
			pickupShotgun = false;
			pickupRifle = false;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == shooting.subGun) 
		{
			pickupSub = true;
		} 
		else if (other.gameObject == shooting.rifleGun) 
		{
			pickupRifle = true;
		} 
		else if (other.gameObject == shooting.sniperGun) 
		{
			pickupSniper = true;
		} 
		else if (other.gameObject == shooting.shotgunGun) 
		{
			pickupShotgun = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == shooting.subGun) 
		{
			pickupSub = false;
		} 
		else if (other.gameObject == shooting.rifleGun) 
		{
			pickupRifle = false;
		} 
		else if (other.gameObject == shooting.sniperGun) 
		{
			pickupSniper = false;
		} 
		else if (other.gameObject == shooting.shotgunGun) 
		{
			pickupShotgun = false;
		}
	}
}