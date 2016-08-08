using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//Public

	public Animator anim;
	public float moveSpeed = 5.0f;
	public float runMultiplier = 1.5f;
	public bool isRunning = false;
	public float attackTimer = 0.0f;

	public bool isJumping = false;
	public float jumpTimer = 0.0f;
	public float lastMoveX;
	public float lastMoveY;

	//Private
	private float sprintWindow = 0.5f;
	private int sprintCount = 0;
	private KeyCode runKey;



	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.Instance.paused)
		{
			movePlayer();
		} 
	}

	void movePlayer() {
		clearMovement();										//Clear movement flags.
		characterInput();
		applyMovement();

	}

	void clearMovement() {
		anim.SetBool("moveDown", false);
		anim.SetBool("moveUp", false);
		anim.SetBool("moveLeft", false);
		anim.SetBool("moveRight", false);

		//Clear attacks
		anim.SetBool("isPunching", false);
	}

	void characterInput() {

		//Jumping
		if (Input.GetKeyDown(KeyCode.Space)) {
			anim.SetTrigger("isJumping");
			isJumping = true;
			this.gameObject.layer = LayerMask.NameToLayer("inAir");
		}

		if(isJumping) {
			jumpTimer += Time.deltaTime;

			if(jumpTimer > 0.5f) {
				isJumping = false;
				this.gameObject.layer = LayerMask.NameToLayer("Default");	//Return to normal layer
				jumpTimer = 0.0f;

				lastMoveX = 0.0f;
				lastMoveY = 0.0f;
			}
		}

		if (Input.GetKeyUp (KeyCode.S ) && !isJumping){			//Clear last move when key is released
			lastMoveY = 0f;
		}

		if (Input.GetKeyUp (KeyCode.W) && !isJumping){
			lastMoveY = 0f;
		}

		if (Input.GetKeyUp (KeyCode.A) && !isJumping){
			lastMoveX = 0.0f;
		}

		if (Input.GetKeyUp (KeyCode.D) && !isJumping){
			lastMoveX = 0.0f;
		}


		//Move down
		if (Input.GetKey(KeyCode.S)) {
			anim.SetBool("moveDown", true);

			if (Input.GetKeyDown(KeyCode.S)) {					//This section controls tracking if you've doubletapped a button.
				if ( sprintWindow > 0 && sprintCount == 1 && !isRunning) {		
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = KeyCode.S;
				} else {
					sprintWindow = 0.5f;
					sprintCount += 1;
				}
			} 
		} 

		//Move up
		if (Input.GetKey(KeyCode.W)) {
			anim.SetBool("moveUp", true);

			if (Input.GetKeyDown(KeyCode.W)) {					//This section controls tracking if you've doubletapped one direction.
				if ( sprintWindow > 0 && sprintCount == 1 && !isRunning) {		
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = KeyCode.W;
				} else {
					sprintWindow = 0.5f;
					sprintCount += 1;
				}
			} 
		} 

		//Move Left
		if (Input.GetKey(KeyCode.A)) {
			anim.SetBool("moveLeft", true);

			if (Input.GetKeyDown(KeyCode.A)) {					//This section controls tracking if you've doubletapped one direction.
				if( sprintWindow > 0 && sprintCount == 1 && !isRunning) {		
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = KeyCode.A;
				} else {
					sprintWindow = 0.5f;
					sprintCount += 1;
				}
			} 
		} 

		//Move right
		if (Input.GetKey(KeyCode.D)) {
			anim.SetBool("moveRight", true);

			if (Input.GetKeyDown(KeyCode.D)) {					//This section controls tracking if you've doubletapped one direction.
				if( sprintWindow > 0 && sprintCount == 1 && !isRunning) {		
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = KeyCode.D;
				} else {
					sprintWindow = 0.5f;
					sprintCount += 1;
				}
			} 
		} 


		//Sprint
		if (sprintWindow > 0) {								//This section resets the doubletap timer.
			sprintWindow -= 1 * Time.deltaTime;
		} else {
			sprintCount = 0;
		}

		if(Input.GetKeyUp(runKey))	//Stop running when you let go of the appropriate key.
		{
			isRunning = false;
		}


		//Attacks
		if (Input.GetKey (KeyCode.J)) 					//While you hold the attack button down, you 'charge' an attack.
		{
			attackTimer += Time.deltaTime;
		}

		if (Input.GetKeyUp (KeyCode.J))						//When you let go of the attack button, it does an attack appropriate for how long you were charging.
		{
			if(attackTimer <= 1.0f)			//Basic attack has a window of 1 second.
			{
				attackTimer = 0.0f;
				anim.SetTrigger("isPunching");
			} else {
				attackTimer = 0.0f;							//End case. Reset attack timer.
			}
		}

	}

	void applyMovement() {

		if(isRunning)
		{
			moveSpeed *= runMultiplier;
		}

		//Movement
		if(!isJumping)										//Normal movement
		{
			if (anim.GetBool("moveDown")) {
				transform.Translate(0, -1 * moveSpeed * Time.deltaTime, 0);
				//lastMoveX = 0;
				lastMoveY = -1 * moveSpeed;
			}
			
			if (anim.GetBool("moveUp")) {
				transform.Translate(0, moveSpeed * Time.deltaTime, 0);
				//lastMoveX = 0;
				lastMoveY = moveSpeed;
			}
			
			if (anim.GetBool("moveLeft")) {
				transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
				lastMoveX = -1 * moveSpeed;
				//lastMoveY = 0;
			}
			
			if (anim.GetBool("moveRight")) {
				transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
				lastMoveX = moveSpeed;
				//lastMoveY = 0;
			}
		} else {											//Jumping movement
			transform.Translate (lastMoveX * Time.deltaTime,lastMoveY * Time.deltaTime, 0);
		}

		if(isRunning)
		{
			moveSpeed /= runMultiplier;
		}

	}
}