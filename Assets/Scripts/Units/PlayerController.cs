using UnityEngine;
using System.Collections;
//[@RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 4f;
	RaycastHit2D hit;

	private Rigidbody2D cachedRigidBody2D;
	private bool isDashing;
	private float dashCooldown = 0.6f;
	private float lastDashTime = -100f;
	private float dashDuration = 0.2f;

	void Start()
	{
		this.cachedRigidBody2D = this.GetComponent<Rigidbody2D>();
		this.isDashing = false;
	}
	
	void FixedUpdate()
	{
		this.movePlayerIfNeeded ();

		this.ajustPlayerRotation ();
	}

	void ajustPlayerRotation()
	{
		Vector3 mouseDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		
		//Player looks at the cursor
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDirection.y - transform.position.y, mouseDirection.x - transform.position.x) * Mathf.Rad2Deg - 180);
	}

	void movePlayerIfNeeded()
	{
		//Dash 
		// Stop the dash if it is in progress
		if (this.isDashing && (Time.time - this.lastDashTime > this.dashDuration)) {
			this.isDashing = false;
		}

		if (this.isDashing) {
			return;
		}

		if (Input.GetKeyDown ("space") && this.canDash())
		{
			this.lastDashTime = Time.time;
			this.isDashing = true;
			this.cachedRigidBody2D.velocity = this.cachedRigidBody2D.velocity * 3;
		}
		//Move the player
		else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) 
		{
			Vector2 inputKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			inputKeys.Normalize ();
			
			this.cachedRigidBody2D.velocity = inputKeys * walkSpeed * Time.deltaTime * 100;
			this.cachedRigidBody2D.angularVelocity = 0;
		} 
		else
		{
			//Stap movin da player
			this.cachedRigidBody2D.velocity = new Vector2 (0,0);
			this.cachedRigidBody2D.angularVelocity = 0;
		}
	}

	bool canDash()
	{
		return Time.time > this.lastDashTime + this.dashCooldown;
	}


	// Debug puprose
	public Vector2 GetPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		//Verify collision with the ennemies
		//To do keyvohn, faire que ça pogne le type de unit
		if (coll.gameObject.tag == "enemy") {
			this.gameObject.GetComponent<UnitHealth>().TakeDamage(10);
			Debug.Log ("Dat collision");
//			coll.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
		}
	}
}
