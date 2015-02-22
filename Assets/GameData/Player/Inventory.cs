using System.Collections;
using System.Collections.Generic;

public class Inventory {
	public List<Handle> Handles;
	public List<PowerModule> PowerModules;
	public List<Barrel> Barrels;
	public List<Magazine> Magazines;
	public List<Extension> Extensions;
	public List<Accessory> Accessories;

	private static Inventory instance;
	public static Inventory Instance {
		get {
			if (instance == null) {
				instance = new Inventory();
				instance.InitializeData();
			}

			return instance;
		}
	}

	private Inventory() {
		this.Handles = new List<Handle>();
		this.PowerModules = new List<PowerModule>();
		this.Barrels = new List<Barrel>();
		this.Magazines = new List<Magazine>();
		this.Extensions = new List<Extension>();
		this.Accessories = new List<Accessory>();
	}

	private void InitializeData() {
		// Fill up inventory with various parts
		// TODO
	}
}
