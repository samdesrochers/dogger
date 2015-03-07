using System.Collections;

public class Magazine : WeaponPart {
	private MagazineType type;
	public MagazineType Type{
		get{
			return this.type;
		}
	}

	public Magazine(string spritePrefabName, MagazineType type, Properties properties)
	: base(spritePrefabName, "Prefabs/Weapons/Weapon Parts/Magazines/", properties) {
		this.type = type;
	}
}
