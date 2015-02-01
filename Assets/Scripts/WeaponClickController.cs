using UnityEngine;
using System.Collections;

public class WeaponClickController : MonoBehaviour {

	public Rigidbody2D ProjectilePrefab;
	public float ProjectileForceModifier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			this.FireBullet();
		}
	}

	private void FireBullet() {
		Vector3 currentPos = this.transform.position;
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = target - currentPos;
		
		direction.z = 0;
		direction.Normalize();
		
		float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		
		Rigidbody2D bulletInstance = (Rigidbody2D)Instantiate(this.ProjectilePrefab, this.transform.position, Quaternion.Euler(0, 0, targetAngle));
		bulletInstance.AddForce(direction * this.ProjectileForceModifier);
	}
}
