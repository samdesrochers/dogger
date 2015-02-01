using UnityEngine;
using System.Collections.Generic;

public class WeaponClickController : MonoBehaviour {
	
	public float ProjectileForceModifier;

	private List<Weapon> weapons;
	private int currentWeapon;

	// Use this for initialization
	void Start () {
		this.weapons = new List<Weapon>();

		Weapon pistol = new Weapon("Prefabs/Projectiles/pistol", 15f, 0.7f);
		Weapon smg = new Weapon("Prefabs/Projectiles/machine_gun", 20f, 0.2f);
		Weapon laserRifle = new Weapon("Prefabs/Projectiles/laser_red", 30f, 1f);
		Weapon photonRifle = new Weapon("Prefabs/Projectiles/photon", 5f, 1.5f);

		this.weapons.Add(pistol);
		this.weapons.Add(smg);
		this.weapons.Add(laserRifle);
		this.weapons.Add(photonRifle);

		this.currentWeapon = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			this.FireBullet();
		}

		if (Input.GetKey(KeyCode.Alpha1)) {
			this.currentWeapon = 0;
		}

		if (Input.GetKey(KeyCode.Alpha2)) {
			this.currentWeapon = 1;
		}

		if (Input.GetKey(KeyCode.Alpha3)) {
			this.currentWeapon = 2;
		}

		if (Input.GetKey(KeyCode.Alpha4)) {
			this.currentWeapon = 3;
		}
	}

	private void FireBullet() {
		Vector3 currentPos = this.transform.position;
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = target - currentPos;
		
		direction.z = 0;
		direction.Normalize();
		
		float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Weapon weapon = this.weapons[this.currentWeapon];

		GameObject bulletInstance = (GameObject)Instantiate(weapon.ProjectilePrefab, this.transform.position, Quaternion.Euler(0, 0, targetAngle));
		Vector2 force = direction * weapon.ProjectileForce * this.ProjectileForceModifier;
		bulletInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
	}
}
