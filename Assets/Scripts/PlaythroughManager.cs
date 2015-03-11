using UnityEngine;
using System.Collections;

public class PlaythroughManager {
	private static PlaythroughManager instance;
	public static PlaythroughManager Instance {
		get {
			if (instance == null) {
				instance = new PlaythroughManager();
			}
			
			return instance;
		}
	}

	private PlayerInfo playerInfo;
	public PlayerInfo PlayerInfo {
		get {
			return this.playerInfo;
		}
	}

	private PlaythroughManager() {
		this.playerInfo = new PlayerInfo(WeaponKit.DUAL_WIELD);

		// TEMP Populate inventory with some weapon parts

		// TEMP Select random parts and assemble weapons
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
		OneHandedWeapon leftGun = new OneHandedWeapon(leftHandle, leftPowerModule, leftPropulsors);
		
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
		OneHandedWeapon rightGun = new OneHandedWeapon(rightHandle, rightPowerModule, rightPropulsors);

		this.playerInfo.Equipment.EquipWeapon(leftGun, WeaponPosition.LEFT);
		this.playerInfo.Equipment.EquipWeapon(rightGun);
	}
}
