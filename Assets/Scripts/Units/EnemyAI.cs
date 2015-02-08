using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	private float rotateTimer;
	private Random rand;

	public float speed;
	private Vector2 walkDirection;
	private Vector3 walkAmount;
	
	void Start () {
		speed = Random.Range (1.0f, 9.0f);
		rotateTimer = Random.Range (1.0f, 10.0f);
		walkDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
	}
	
	void Update () {
		
		rotateTimer -= Time.deltaTime;
		if ( rotateTimer < 0 )
		{
			speed = Random.Range (1.0f, 9.0f);
			rotateTimer = Random.Range (1.0f, 10.0f);
			walkDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		}

		walkAmount.x = walkDirection.x * speed * Time.deltaTime;
		walkAmount.y = walkDirection.y * speed * Time.deltaTime;

		transform.Translate(walkAmount);
	}
}
