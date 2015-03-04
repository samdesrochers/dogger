using System.Collections;

public class PowerModule : WeaponPart {
	private int barrelSlots;
	public int BarrelSlots {
		get {
			return this.barrelSlots;
		}
	}

	public PowerModule(string spritePrefabName, Properties properties, int barrelSlots)
	: base(spritePrefabName, "Prefabs/Weapons/Weapon Parts/Power Modules/", properties) {
		this.barrelSlots = barrelSlots;
	}
}
