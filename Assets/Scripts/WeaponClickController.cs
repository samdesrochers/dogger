using UnityEngine;
using System.Collections.Generic;

public class WeaponClickController : MonoBehaviour {
	public float minAimDistance = 1.4f;
	
	private List<OldWeapon> weapons;
	private int currentWeapon;
	
	// Use this for initialization
	void Start () {
		this.weapons = new List<OldWeapon>();
		
		OldWeapon pistol = new OldWeapon("Prefabs/Projectiles/pistol", 20f, 0.0f);
		OldWeapon smg = new OldWeapon("Prefabs/Projectiles/machine_gun", 30f, 0.1f);
		OldWeapon laserRifle = new OldWeapon("Prefabs/Projectiles/laser_red", 35f, 0.5f);
		OldWeapon photonRifle = new OldWeapon("Prefabs/Projectiles/photon", 5f, 0.75f);
		
		this.weapons.Add(pistol);
		this.weapons.Add(smg);
		this.weapons.Add(laserRifle);
		this.weapons.Add(photonRifle);
		
		this.currentWeapon = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Transform leftGun = this.transform.Find("LeftGunContainer");
		Vector3 mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseTarget.z = 0f;
		
		// Orient weapons towards cursor
		float aimDistance = Vector3.Distance(leftGun.position, mouseTarget);
		if (aimDistance > this.minAimDistance) {
			Vector3 direction = mouseTarget - leftGun.position;		
			direction.Normalize ();
			
			float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			leftGun.transform.rotation = Quaternion.Euler(0f, 0f, targetAngle - 180.0f);
		}
		
		// Fire weapon
		if (Input.GetButton("Fire1")) {
			OldWeapon weapon = this.weapons[this.currentWeapon];
			if (weapon.CanFire()) {
				weapon.Fire(leftGun, mouseTarget);
			}
		}
		
		// Release trigger
		if (Input.GetButtonUp ("Fire1")) {
			foreach(OldWeapon w in this.weapons) {
				w.ReleaseTrigger();
			}
		}
		
		// Change weapon
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
