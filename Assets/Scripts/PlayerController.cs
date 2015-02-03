using UnityEngine;
using System.Collections;
//[@RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 6f;
	RaycastHit2D hit;

	private Rigidbody2D cachedRigidBody2D;

	void Start()
	{
		this.cachedRigidBody2D = this.GetComponent<Rigidbody2D>();
	}

	
	void FixedUpdate()
	{
		Vector2 inputKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		//Move the player
		if (inputKeys.x != 0 || inputKeys.y != 0) {
			var direction = inputKeys * walkSpeed * Time.deltaTime;

			this.rigidbody2D.MovePosition ((Vector2)transform.position + direction);
		} 
		else 
		{
			//Stap movin da player
			this.rigidbody2D.velocity = new Vector2 (0,0);
			this.rigidbody2D.angularVelocity = 0;
		}

		Vector3 mouseDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition );

		//Player looks at the cursor
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDirection.y - transform.position.y, mouseDirection.x - transform.position.x) * Mathf.Rad2Deg - 180);
	}
}

