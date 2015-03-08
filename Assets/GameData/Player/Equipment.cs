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
		Handle leftHandle = new Handle("default", new Properties());
		PowerModule leftPowerModule = new PowerModule("default", new Properties(), 2);
		Propulsor[] leftPropulsors = new Propulsor[]
		{
			new Propulsor(new Barrel("default", BarrelType.MACHINE_GUN, new Properties(baseVelocity: 15f, baseShotsPerSecond: 8f)),
			              new Magazine("default", MagazineType.METAL, new Properties()),
			              new Extension("default", new Properties()), 
			              new Accessory("default", new Properties())),
			new Propulsor(new Barrel("default", BarrelType.PISTOL, new Properties(baseVelocity: 10f)),
			              new Magazine("default", MagazineType.METAL, new Properties()),
			              new Extension("default", new Properties()), 
			              new Accessory("default", new Properties()))
		};
		this.leftGun = new OneHandedWeapon(leftHandle, leftPowerModule, leftPropulsors);
		
		// Create right gun
		Handle rightHandle = new Handle("default", new Properties());
		PowerModule rightPowerModule = new PowerModule("default", new Properties(), 2);
		Propulsor[] rightPropulsors = new Propulsor[]
		{
			new Propulsor(new Barrel("default", BarrelType.PISTOL, new Properties(baseVelocity: 10f)),
			              new Magazine("default", MagazineType.METAL, new Properties()),
			              new Extension("default", new Properties()), 
			              new Accessory("default", new Properties())),
			new Propulsor(new Barrel("default", BarrelType.MACHINE_GUN, new Properties(baseVelocity: 10f, baseShotsPerSecond: 4f)),
			              new Magazine("default", MagazineType.METAL, new Properties()),
			              new Extension("default", new Properties()), 
			              new Accessory("default", new Properties()))
		};
		this.rightGun = new OneHandedWeapon(rightHandle, rightPowerModule, rightPropulsors);
	}
}
