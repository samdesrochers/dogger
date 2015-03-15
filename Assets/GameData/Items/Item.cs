using System;

public class Item {

	public enum ItemType {
		Barrel = 1,
		Extension, 
		Handle, 
		Magazine, 
		Powermodule, 
		Coins, 
		Potion
	};
	
	public int id;
	public string name;
	public string description;
	public ItemType itemType;
}
