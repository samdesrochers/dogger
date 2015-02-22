using UnityEngine;
using System.Collections;

public class EquipmentController : MonoBehaviour {
	private enum WeaponType {DUAL_WIELD, TWO_HANDED};

	private WeaponType currentWeaponType;

	// Use this for initialization
	void Start () {
		// Equip default weapons...
		// ...Set to dual wield
		this.currentWeaponType = WeaponType.DUAL_WIELD;

		// ...Create containers
		GameObject leftGunContainerObject = Instantiate(Resources.Load("Prefabs/Weapons/LeftGunContainer")) as GameObject;
		leftGunContainerObject.name = "LeftGun";
		Transform leftGunContainerTransform = leftGunContainerObject.GetComponent<Transform>();
		leftGunContainerTransform.parent = this.GetComponent<Transform>();

		GameObject rightGunContainerObject = Instantiate(Resources.Load("Prefabs/Weapons/RightGunContainer")) as GameObject;
		rightGunContainerObject.name = "RightGun";
		Transform rightGunContainerTransform = rightGunContainerObject.GetComponent<Transform>();
		rightGunContainerTransform.parent = this.GetComponent<Transform>();

		// ...Create left gun
		Handle leftHandle = new Handle("default");
		PowerModule leftPowerModule = new PowerModule("default");
		Barrel leftBarrel = new Barrel("default");
		Magazine leftMagazine = new Magazine("default");
		Extension leftExtension = new Extension("default");
		Accessory leftAccessory = new Accessory("default");
		Propulsor leftPropulsor = new Propulsor(leftBarrel, leftMagazine, leftExtension, leftAccessory);
		OneHandedWeapon leftGun = new OneHandedWeapon(leftHandle, leftPowerModule, leftPropulsor);

		// ...Create right gun
		Handle rightHandle = new Handle("default");
		PowerModule rightPowerModule = new PowerModule("default");
		Barrel rightBarrel = new Barrel("default");
		Magazine rightMagazine = new Magazine("default");
		Extension rightExtension = new Extension("default");
		Accessory rightAccessory = new Accessory("default");
		Propulsor rightPropulsor = new Propulsor(rightBarrel, rightMagazine, rightExtension, rightAccessory);
		OneHandedWeapon rightGun = new OneHandedWeapon(rightHandle, rightPowerModule, rightPropulsor);

		// ...Draw weapons
		this.DrawWeapon (leftGunContainerTransform, leftGun);
		this.DrawWeapon (rightGunContainerTransform, rightGun);
	}
	
	// Update is called once per frame
	void Update () {
		// Select weapon type
		// TODO

		// Select weapon parts
		// TODO
	}

	private void DrawWeapon(Transform weaponContainer, BaseWeapon weapon) {
		if (weapon is OneHandedWeapon) {
			this.DrawOneHandedWeapon (weaponContainer, weapon as OneHandedWeapon);
		} else {
			this.DrawTwoHandedWeapon(weaponContainer, weapon as TwoHandedWeapon);
		}
	}

	private void DrawOneHandedWeapon(Transform weaponContainer, OneHandedWeapon weapon) {
		// Draw the handle
		GameObject handleObject = Instantiate(Resources.Load(weapon.handle.SpriteFullPath)) as GameObject;
		handleObject.name = "Handle";
		Transform handleTransform = handleObject.GetComponent<Transform>();
		handleTransform.parent = weaponContainer;
		handleTransform.localPosition = new Vector3(0f, 0f, 0f);
	}

	private void DrawTwoHandedWeapon(Transform weaponContainer, TwoHandedWeapon weapon) {
	}
}
