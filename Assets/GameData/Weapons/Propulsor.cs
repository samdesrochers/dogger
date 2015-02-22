using UnityEngine;
using System.Collections;

public class Propulsor {
	private Barrel barrel;
	private Magazine magazine;
	private Extension extension;
	private Accessory accessory;

	private float cooldown;
	private float projectileForce;

	private float lastFired;
	private bool isAuto;
	private bool isTriggerDown;

	public Propulsor(Barrel barrel, Magazine magazine, Extension extension, Accessory accessory) {
		this.barrel = barrel;
		this.magazine = magazine;
		this.extension = extension;
		this.accessory = accessory;
	}

	public bool CanFire() {
		if (this.isAuto) {
			return Time.time > this.lastFired + this.cooldown;
		} else {
			return !this.isTriggerDown;
		}
	}

	public void ReleaseTrigger() {
		this.isTriggerDown = false;
	}
}
