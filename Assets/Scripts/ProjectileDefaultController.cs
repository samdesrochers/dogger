using UnityEngine;
using System.Collections;

public class ProjectileDefaultController : MonoBehaviour {

	// Time before the bullet disappears, in seconds
	public float TimeToLive = 5f;

	private Vector2 direction;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, this.TimeToLive);
		
		direction = this.GetComponent<Rigidbody2D>().velocity;	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D coll
	                     ) {
		// To change keyvohn
		if (coll.gameObject.tag != "Player") {
			// Blood splash
			float tireAngle = Mathf.Rad2Deg*Mathf.Atan2(-direction.y,direction.x);

			GameObject e = (GameObject)Instantiate(Resources.Load("Prefabs/Projectiles/bloodSplash"), coll.transform.position, Quaternion.Euler(tireAngle,90,0)); 
			ParticleSystem spashParticleSystem = e.GetComponent<ParticleSystem>();
			spashParticleSystem.gameObject.SetActive(true);
			spashParticleSystem.enableEmission = true;
			spashParticleSystem.Play();
			Destroy(spashParticleSystem.gameObject, 0.1f);

			coll.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
			Destroy (this.gameObject);	
		}
	}
}
