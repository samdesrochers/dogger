using UnityEngine;
using System.Collections;

public class ProjectileDefaultController : MonoBehaviour {

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

	void OnCollisionEnter2D(Collision2D coll
	                     ) {
		// To change keyvohn
		if (coll.gameObject.tag != "Player") {
			Debug.Log("Dam son dat bullet hit !!!");
			coll.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.RequireReceiver);
			Destroy (this.gameObject);	
		}
	}
}
