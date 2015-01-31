using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;

	void Start () {
		target = GameObject.FindWithTag ("Player").transform;
	}
	
	void Update () {
	
		// Adjust camera on player's position
//		Camera.main.transform.position = new Vector3(playerRef.position.x, 
//		                                             playerRef.position.y, 
//		                                             Camera.main.transform.position.z);

		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}
