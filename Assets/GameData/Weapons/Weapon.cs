using UnityEngine;
using System.Collections;

public class Weapon {
	
	private Object projectilePrefab;
	private float projectileForce;
	private float cooldown;	// Weapon cooldown, in seconds
	private float lastFired;
	private bool isAuto;
	private bool isTriggerDown;
	
	public Weapon(string projPrefabPath, float projForce, float cooldown) {
		this.projectilePrefab = Resources.Load(projPrefabPath);
		this.projectileForce = projForce;
		this.cooldown = cooldown;
		this.lastFired = 0.0f;
		this.isAuto = cooldown == 0.0f ? false : true;
		this.isTriggerDown = false;
	}
	
	public bool CanFire() {
		if (this.isAuto) {
			return Time.time > this.lastFired + this.cooldown;
		} else {
			return !this.isTriggerDown;
		}
	}
	
	public void Fire(Transform weaponTransform, Vector3 target) {
		Vector3 direction = weaponTransform.rotation * Vector3.left;
		Vector2 force = direction * this.projectileForce;
		
		GameObject bulletInstance = (GameObject)MonoBehaviour.Instantiate(this.projectilePrefab, weaponTransform.position, weaponTransform.rotation);
		bulletInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
		bulletInstance.GetComponent<SpriteRenderer>().sortingLayerName = "ProjectilesLayer";
		this.lastFired = Time.time;
		this.isTriggerDown = true;
	}
	
	public void ReleaseTrigger() {
		this.isTriggerDown = false;
	}
}
