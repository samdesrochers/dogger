using UnityEngine;
using System.Collections;

public class Weapon {

	public Object ProjectilePrefab;
	public float ProjectileForce;
	public float Cooldown;	// Weapon cooldown, in seconds

	public Weapon(string projPrefabPath, float projForce, float cooldown) {
		this.ProjectilePrefab = Resources.Load(projPrefabPath);
		this.ProjectileForce = projForce;
		this.Cooldown = cooldown;
	}
}
