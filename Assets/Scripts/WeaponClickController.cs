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
		if (Input.GetButton("Fire1")) {
			Weapon weapon = this.weapons[this.currentWeapon];
			if (weapon.CanFire()) {
				Vector3 position = this.transform.position;
				Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				weapon.Fire(position, target, this.ProjectileForceModifier);
			}
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
}
