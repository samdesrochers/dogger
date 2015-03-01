using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	private float rotateTimer;
	private Random rand;

	public float speed;


	FOV2DEyes eyes;
	FOV2DVisionCone visionCone;
	public float visibilityDistance;
	public float fieldOfViewDegrees;

	private Vector2 walkDirection;
	private Vector3 walkAmount;
	public GameObject Player;
	
	void Start () {
		speed = Random.Range (1.0f, 9.0f);
		rotateTimer = Random.Range (1.0f, 10.0f);
		walkDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

		visibilityDistance = 10;
		fieldOfViewDegrees = 30;

		eyes = GetComponentInChildren<FOV2DEyes>();
		visionCone = GetComponentInChildren<FOV2DVisionCone>();
	}
	
	void Update () {
		bool playerInView = false;
		
		foreach (RaycastHit2D hit in eyes.hits)
		{

			if (hit.transform && hit.transform.gameObject.CompareTag ("Player"))
			{
				Debug.Log ("Wassup donger");
				playerInView = true;
			}
		}
		
		if (playerInView)
		{
			visionCone.status = FOV2DVisionCone.Status.Alert;
		}
		else
		{
			visionCone.status = FOV2DVisionCone.Status.Idle;
		}


		//To do keyvohn, remettre ça 

//		rotateTimer -= Time.deltaTime;
//		if ( rotateTimer < 0 )
//		{
//			speed = Random.Range (1.0f, 9.0f);
//			rotateTimer = Random.Range (1.0f, 10.0f);
//			walkDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
//		}
//
//		walkAmount.x = walkDirection.x * speed * Time.deltaTime;
//		walkAmount.y = walkDirection.y * speed * Time.deltaTime;
//
//		transform.Translate(walkAmount);

//		if (this.CanSeePlayer ()) {
//			Debug.Log ("Can see player !");
//		}
	}
//
//	protected bool CanSeePlayer()
//	{
//		RaycastHit hit;
//		Vector3 rayDirection = Player.transform.position - transform.position;
//		
//		if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
//		{
//			// Detect if player is within the field of view
//			if (Physics2D.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
//			{
//				return (hit.transform.CompareTag("Player"));
//			}
//		}
//		
//		return false;
//	}
}
