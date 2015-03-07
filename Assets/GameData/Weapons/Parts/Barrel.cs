using System.Collections;

public class Barrel : WeaponPart{
	private BarrelType type;
	public BarrelType Type{
		get{
			return this.type;
		}
	}

	public Barrel(string spritePrefabName, BarrelType type, Properties properties)
	: base(spritePrefabName, "Prefabs/Weapons/Weapon Parts/Barrels/", properties) {
		this.type = type;
	}
}
