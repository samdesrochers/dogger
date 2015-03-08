using System;

public class ProjectileEmitterContainer {
	private static string[,] emitterPrefabs;
	private static Type[,] emitterControllers;

	public static string GetPrefabPath(Propulsor propulsor) {
		if (emitterPrefabs == null) {
			InitializePrefabsArray();
		}

		int barrelType = (int)propulsor.Barrel.Type;
		int magazineType = (int)propulsor.Magazine.Type;

		return emitterPrefabs[barrelType, magazineType];
	}

	public static Type GetControllerClass(Propulsor propulsor) {
		if (emitterControllers == null) {
			InitializeControllersArray();
		}
		
		int barrelType = (int)propulsor.Barrel.Type;
		int magazineType = (int)propulsor.Magazine.Type;
		
		return emitterControllers[barrelType, magazineType];
	}

	private static void InitializePrefabsArray() {
		emitterPrefabs = new string[Enum.GetNames(typeof(BarrelType)).Length, Enum.GetNames(typeof(MagazineType)).Length];

		// Pistol emitters
		string pistolEmitterRootPath = "Prefabs/Weapons/Projectile Emitters/Pistol/";

		emitterPrefabs[(int)BarrelType.PISTOL, (int)MagazineType.METAL] = pistolEmitterRootPath + "MetalPistol";

		// Machine Gun emitters
		string machineGunEmitterRootPath = "Prefabs/Weapons/Projectile Emitters/Machine Gun/";
		
		emitterPrefabs[(int)BarrelType.MACHINE_GUN, (int)MagazineType.METAL] = machineGunEmitterRootPath + "MetalMachineGun";

		// Shotgun emitters
		// Rail Gun emitters
		// Grenade Launcher emitters
	}

	private static void InitializeControllersArray() {
		emitterControllers = new Type[Enum.GetNames(typeof(BarrelType)).Length, Enum.GetNames(typeof(MagazineType)).Length];
		
		// Pistol controllers
		emitterControllers[(int)BarrelType.PISTOL, (int)MagazineType.METAL] = typeof(MetalPistolEmitterController);
		
		// Machine Gun controllers
		emitterControllers[(int)BarrelType.MACHINE_GUN, (int)MagazineType.METAL] = typeof(MetalMachineGunEmitterController);

		// Shotgun controllers
		// Rail Gun controllers
		// Grenade Launcher controllers
	}
}
