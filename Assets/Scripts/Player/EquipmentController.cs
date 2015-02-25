using UnityEngine;
using System.Collections;

public class EquipmentController : MonoBehaviour {
	private enum WeaponPosition {LEFT, RIGHT, TWO_HANDED};
	private enum WeaponType {DUAL_WIELD, TWO_HANDED};

	private WeaponType currentWeaponType;

	// Use this for initialization
	void Start () {
		// Equip default weapons...
		// ...Set to dual wield
		this.currentWeaponType = WeaponType.DUAL_WIELD;

		// ...Create left gun
		Handle leftHandle = new Handle("default");
		PowerModule leftPowerModule = new PowerModule("default");
		Propulsor[] leftPropulsors = new Propulsor[]
		{
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default")),
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default"))
		};
		Equipment.Instance.LeftGun = new OneHandedWeapon(leftHandle, leftPowerModule, leftPropulsors);

		// ...Create right gun
		Handle rightHandle = new Handle("default");
		PowerModule rightPowerModule = new PowerModule("default");
		Propulsor[] rightPropulsors = new Propulsor[]
		{
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default")),
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default"))
		};
		Equipment.Instance.RightGun = new OneHandedWeapon(rightHandle, rightPowerModule, rightPropulsors);

		// ...Draw weapons
		this.DrawWeapons();
	}
	
	// Update is called once per frame
	void Update () {
		// Select weapon type
		// TODO

		// Select weapon parts
		// TODO
	}

	private Transform CreateGunContainer(WeaponPosition position) {
		string prefabPath = "Prefabs/Weapons/";
		string objectName = "";

		switch (position) {
		case WeaponPosition.LEFT:
			prefabPath += "LeftGunContainer";
			objectName = "LeftGun";
			break;

		case WeaponPosition.RIGHT:
			prefabPath += "RightGunContainer";
			objectName = "RightGun";
			break;

		case WeaponPosition.TWO_HANDED:
			prefabPath += "TwoHandedGunContainer";
			objectName = "RightGun";
			break;
		}

		GameObject gunContainerObject = Instantiate(Resources.Load(prefabPath)) as GameObject;
		gunContainerObject.name = objectName;
		Transform gunContainerTransform = gunContainerObject.GetComponent<Transform>();
		gunContainerTransform.parent = this.GetComponent<Transform>();
		return gunContainerTransform;
	}

	private void DrawWeapons() {
		Transform rightGunContainerTransform = this.CreateGunContainer(WeaponPosition.RIGHT);

		if (this.currentWeaponType == WeaponType.DUAL_WIELD) {
			Transform leftGunContainerTransform = this.CreateGunContainer(WeaponPosition.LEFT);
			this.DrawOneHandedWeapon(leftGunContainerTransform, Equipment.Instance.LeftGun as OneHandedWeapon);
			this.DrawOneHandedWeapon(rightGunContainerTransform, Equipment.Instance.RightGun as OneHandedWeapon);
		} else {
			this.DrawTwoHandedWeapon(rightGunContainerTransform, Equipment.Instance.RightGun as TwoHandedWeapon);
		}
	}

	private void DrawOneHandedWeapon(Transform weaponContainer, OneHandedWeapon weapon) {
		// Draw the handle
		GameObject handleObject = Instantiate(Resources.Load(weapon.Handle.SpriteFullPath)) as GameObject;
		handleObject.name = "Handle";
		Transform handleTransform = handleObject.GetComponent<Transform>();
		handleTransform.parent = weaponContainer;
		handleTransform.localPosition = new Vector3(0f, 0f, 0f);
		handleTransform.localRotation = Quaternion.Euler(0f, 0f, 90.0f);
	}

	private void DrawTwoHandedWeapon(Transform weaponContainer, TwoHandedWeapon weapon) {
	}
}
