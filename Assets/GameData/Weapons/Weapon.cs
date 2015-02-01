using UnityEngine;
using System.Collections;

public class Weapon {

	private Object projectilePrefab;
	private float projectileForce;
	private float cooldown;	// Weapon cooldown, in seconds
	private float lastFired;

	public Weapon(string projPrefabPath, float projForce, float cooldown) {
		this.projectilePrefab = Resources.Load(projPrefabPath);
		this.projectileForce = projForce;
		this.cooldown = cooldown;
	}

	public bool CanFire() {
		return Time.time > this.lastFired + this.cooldown;
	}

	public void Fire(Vector3 position, Vector3 target, float forceModifier) {
		Vector3 direction = target - position;
		
		direction.z = 0;
		direction.Normalize();
		
		float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;		
		GameObject bulletInstance = (GameObject)MonoBehaviour.Instantiate(this.projectilePrefab, position, Quaternion.Euler(0, 0, targetAngle));
		Vector2 force = direction * this.projectileForce * forceModifier;

		bulletInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
		this.lastFired = Time.time;
	}
}
