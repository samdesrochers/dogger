using UnityEngine;
using System.Collections;

public class BulletDefaultController : MonoBehaviour {

	// Time before the bullet disappears, in seconds
	public float TimeToLive = 5f;

	private Vector3 direction;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, this.TimeToLive);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
