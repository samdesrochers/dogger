using System;

public class WeaponPart : Item {
	public Properties Properties;

	protected readonly string spritePrefabFolderPath;
	protected readonly string spritePrefabName;
	
	public string PrefabFullPath {
		get {
			return this.spritePrefabFolderPath + this.spritePrefabName;
		}
	}

	public WeaponPart(string spritePrefabName, string spritePrefabFolderPath, Properties properties)
	{
		this.spritePrefabName = spritePrefabName;
		this.spritePrefabFolderPath = spritePrefabFolderPath;
		this.Properties = properties;
	}
}
