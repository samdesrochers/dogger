using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	private float rotateTimer;
	private Random rand;

	public float speed;
	public float visionDistance;
	public GameObject Player;
	private Vector2 walkDirection;
	private Vector3 walkAmount;
	private bool didSeePlayer;
	private bool shouldShoot;
	private Rigidbody2D cachedRigidBody2D;
	
	void Start () {
		speed = Random.Range (1.0f, 9.0f);
		rotateTimer = Random.Range (1.0f, 10.0f);
		walkDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		visionDistance = Random.Range (15.0f, 30.0f);
		didSeePlayer = false;
		cachedRigidBody2D = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (didSeePlayer) {
			//Move toward the player

			walkDirection =  this.Player.transform.position - this.transform.position ;
			walkDirection.Normalize();
		} else {
			rotateTimer -= Time.deltaTime;
			if ( rotateTimer < 0 )
			{
				speed = Random.Range (1.0f, 9.0f);
				rotateTimer = Random.Range (1.0f, 10.0f);
				walkDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
			}
		}
		cachedRigidBody2D.velocity = walkDirection * speed * Time.deltaTime * 20;
		cachedRigidBody2D.angularVelocity = 0;

		//Check if the player is close from the enemy
		if (!didSeePlayer)
		{
			if (Vector2.Distance (this.transform.position, this.Player.transform.position) < this.visionDistance) {
				didSeePlayer = true;

				//Check if the player can be seen by the enemy
				RaycastHit2D hitInfo = Physics2D.Raycast(this.transform.position, this.Player.transform.position, this.visionDistance);
				if (hitInfo.collider)
				{
					if(hitInfo.collider.gameObject == this.Player)
					{
						Debug.Log( "Can see player!" );
						//Go full retard
						shouldShoot = true;
					}
				}
			}
		}
	}
}
