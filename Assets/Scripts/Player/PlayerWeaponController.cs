using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour {
	public float MinimumAimDistance = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseTarget.z = 0f;

		// Orient weapons towards cursor
		float aimDistance = Vector3.Distance(this.transform.position, mouseTarget);
		if (aimDistance > this.MinimumAimDistance) {
			if (Equipment.Instance.CurrentWeaponKit == WeaponKit.DUAL_WIELD) {
				this.PointWeaponTowardsMouse(this.transform.FindChild("LeftGun"), mouseTarget);
			}

			this.PointWeaponTowardsMouse(this.transform.FindChild("RightGun"), mouseTarget);
		}
		
		// Fire weapon
		/*if (Input.GetButton("Fire1")) {
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
		}*/
	}

	private void PointWeaponTowardsMouse(Transform weaponContainerTransform, Vector3 mouseTarget) {
		Vector3 direction = mouseTarget - weaponContainerTransform.position;		
		direction.Normalize ();
		
		float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		weaponContainerTransform.transform.rotation = Quaternion.Euler(0f, 0f, targetAngle - 180.0f);
	}
}
