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
		if (Input.GetMouseButton(0)) {
			BaseWeapon weapon = Equipment.Instance.RightGun;

			if (weapon.CanFire()) {
				// Register weapon fired
				weapon.WeaponFired();

				// Start the projectile emitter
				IProjectileEmitterController emitterController = this.FindEmitterController("Right", weapon);
				emitterController.StartEmitting(weapon.ActivePropulsor.Properties);
			}
		}

		if (Input.GetMouseButton(1)) {
			BaseWeapon weapon = Equipment.Instance.LeftGun;
			
			if (weapon != null && weapon.CanFire()) {
				// Register weapon fired
				weapon.WeaponFired();
				
				// Start the projectile emitter
				IProjectileEmitterController emitterController = this.FindEmitterController("Left", weapon);
				emitterController.StartEmitting(weapon.ActivePropulsor.Properties);
			}
		}

		// Release triggers
		if (Input.GetMouseButtonUp(0)) {
			BaseWeapon weapon = Equipment.Instance.RightGun;

			// Mark trigger as released
			weapon.TriggerReleased();

			// Stop the projectile emitter
			IProjectileEmitterController emitterController = this.FindEmitterController("Right", weapon);
			emitterController.StopEmitting();
		}

		if (Input.GetMouseButtonUp(1)) {
			BaseWeapon weapon = Equipment.Instance.LeftGun;

			// Mark trigger as released
			weapon.TriggerReleased();
			
			// Stop the projectile emitter
			IProjectileEmitterController emitterController = this.FindEmitterController("Left", weapon);
			emitterController.StopEmitting();
		}
		
		// Change weapon
		int newFireMode = -1;

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			newFireMode = 0;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			newFireMode = 1;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			newFireMode = 2;
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			newFireMode = 3;
		}

		if (newFireMode != -1) {
			if(Equipment.Instance.CurrentWeaponKit == WeaponKit.TWO_HANDED && newFireMode <= 2) {
				Equipment.Instance.RightGun.SwitchFireMode(newFireMode);
			} else if (newFireMode <= 1) {
				Equipment.Instance.RightGun.SwitchFireMode(newFireMode);
			} else {
				Equipment.Instance.LeftGun.SwitchFireMode(newFireMode - 2);
			}
		}
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
			string emitterrObjectPath = string.Format("{0}Gun/{1}/ProjectileEmitter", propulsorKeyPrefix, propulsorName);
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
