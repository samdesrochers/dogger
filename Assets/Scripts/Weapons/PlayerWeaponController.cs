using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class PlayerWeaponController : MonoBehaviour {
	public float MinimumAimDistance = 2f;

	private Dictionary<string, IProjectileEmitterController> emitterControllers;

	// Use this for initialization
	void Start () {
		emitterControllers = new Dictionary<string, IProjectileEmitterController>();
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

		// Fire weapons
		if (Input.GetButton("Fire1")) {
			BaseWeapon weapon = Equipment.Instance.RightGun;

			if (weapon.CanFire()) {
				// Register weapon fired
				weapon.WeaponFired();

				// Start the projectile emitter
				IProjectileEmitterController emitterController = this.FindEmitterController("Right", weapon);
				emitterController.StartEmitting(weapon.ActivePropulsor.Properties);
			}
		}

		// TODO Right mouse button

		// Release triggers
		if (Input.GetButtonUp ("Fire1")) {
			BaseWeapon weapon = Equipment.Instance.RightGun;
			// Mark trigger as released
			weapon.TriggerReleased();

			// Stop the projectile emitter
			IProjectileEmitterController emitterController = this.FindEmitterController("Right", weapon);
			emitterController.StopEmitting();
		}

		// TODO Right mouse button

		// Fire main weapon
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
		direction.z = 0f;
		direction.Normalize();
		
		float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		weaponContainerTransform.transform.rotation = Quaternion.Euler(0f, 0f, targetAngle - 180.0f);
	}

	private IProjectileEmitterController FindEmitterController(string propulsorKeyPrefix, BaseWeapon weapon) {		
 		string propulsorName = weapon.ActivePropulsorGameObjectName;
		string propulsorKey = propulsorKeyPrefix + propulsorName;

		if (!this.emitterControllers.ContainsKey(propulsorKey)) {
			string emitterrObjectPath = string.Format("RightGun/{0}/ProjectileEmitter", propulsorName);
			Transform emitterTransform = this.transform.FindChild(emitterrObjectPath);
			Type emitterControllerClass = ProjectileEmitterContainer.GetControllerClass(weapon.ActivePropulsor);
			
			MethodInfo getComponentMethod = typeof(Transform).GetMethod("GetComponent", Type.EmptyTypes);
			MethodInfo getEmitterComponentMethod = getComponentMethod.MakeGenericMethod(emitterControllerClass);
			object emitterComponent = getEmitterComponentMethod.Invoke(emitterTransform, null);
			IProjectileEmitterController emitterController = emitterComponent as IProjectileEmitterController;

			this.emitterControllers[propulsorKey] = emitterController;
		}

		return this.emitterControllers[propulsorKey];
	}
}
