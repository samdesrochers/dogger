using System;

public class WeaponPart {
	protected readonly string spritePrefabFolderPath;
	protected readonly string spritePrefabName;

	public string PrefabFullPath {
		get {
			return this.spritePrefabFolderPath + this.spritePrefabName;
		}
	}

	public Properties Properties;

	public WeaponPart(string spritePrefabName, string spritePrefabFolderPath, Properties properties)
	{
		this.spritePrefabName = spritePrefabName;
		this.spritePrefabFolderPath = spritePrefabFolderPath;
		this.Properties = properties;
	}
}
