using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	private Transform playerRef;

	void Start () {
		playerRef = GameObject.FindWithTag ("Player").transform;
	}
	
	void Update () {
	
		// Adjust camera on player's position
		Camera.main.transform.position = new Vector3(playerRef.position.x, 
		                                             playerRef.position.y, 
		                                             Camera.main.transform.position.z);
	}
}
