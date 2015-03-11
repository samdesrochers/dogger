using System.Collections;

public class Equipment {
	private WeaponKit currentWeaponKit;
	public WeaponKit CurrentWeaponKit {
		get {
			return this.currentWeaponKit;
		}
	}
	private BaseWeapon leftGun;
	public BaseWeapon LeftGun {
		get {
			return this.leftGun;
		}
	}

	private BaseWeapon rightGun;
	public BaseWeapon RightGun {
		get {
			return this.rightGun;
		}
	}

	public Equipment(WeaponKit weaponKit) {
		this.currentWeaponKit = weaponKit;
	}

	public void EquipWeapon(BaseWeapon weapon, WeaponPosition position = WeaponPosition.RIGHT) {
		if (position == WeaponPosition.RIGHT) {
			this.rightGun = weapon;
		} else {
			this.leftGun = weapon;
		}
	}
}
