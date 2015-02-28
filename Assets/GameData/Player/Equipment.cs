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

	private static Equipment instance;
	public static Equipment Instance {
		get {
			if (instance == null) {
				instance = new Equipment();
			}
			
			return instance;
		}
	}
	
	private Equipment() {
		this.currentWeaponKit = WeaponKit.DUAL_WIELD;
		
		// Create left gun
		Handle leftHandle = new Handle("default");
		PowerModule leftPowerModule = new PowerModule("default", 2);
		Propulsor[] leftPropulsors = new Propulsor[]
		{
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default")),
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default"))
		};
		this.leftGun = new OneHandedWeapon(leftHandle, leftPowerModule, leftPropulsors);
		
		// Create right gun
		Handle rightHandle = new Handle("default");
		PowerModule rightPowerModule = new PowerModule("default", 2);
		Propulsor[] rightPropulsors = new Propulsor[]
		{
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default")),
			new Propulsor(new Barrel("default"), new Magazine("default"), new Extension("default"), new Accessory("default"))
		};
		this.rightGun = new OneHandedWeapon(rightHandle, rightPowerModule, rightPropulsors);
	}
}
