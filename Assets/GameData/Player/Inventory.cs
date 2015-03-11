using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
	private List<Handle> handles;
	private List<PowerModule> powerModules;
	private List<Barrel> barrels;
	private List<Magazine> magazines;
	private List<Extension> extensions;
	private List<Accessory> accessories;

	public Inventory() {
		this.handles = new List<Handle>();
		this.powerModules = new List<PowerModule>();
		this.barrels = new List<Barrel>();
		this.magazines = new List<Magazine>();
		this.extensions = new List<Extension>();
		this.accessories = new List<Accessory>();
	}

//	public void AddWeaponPart(WeaponPart part) {
//		Type partType = part.GetType();
//
//		switch(partType) {
//		case typeof(Handle):
//			this.AddWeaponPartInternal(part as Handle);
//			break;
//		}
//	}

	public void AddWeaponPart(Handle handle) {
		this.handles.Add(handle);
	}

	public void AddWeaponPart(PowerModule powerModule) {
		this.powerModules.Add(powerModule);
	}

	public void AddWeaponPart(Barrel barrel) {
		this.barrels.Add(barrel);
	}

	public void AddWeaponPart(Magazine magazine) {
		this.magazines.Add(magazine);
	}

	public void AddWeaponPart(Extension extension) {
		this.extensions.Add(extension);
	}

	public void AddWeaponPart(Accessory accessory) {
		this.accessories.Add(accessory);
	}
}
