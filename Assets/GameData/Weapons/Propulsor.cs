using UnityEngine;
using System.Collections;

public class Propulsor {
	private Barrel barrel;
	public Barrel Barrel {
		get {
			return this.barrel;
		}
	}

	private Magazine magazine;
	public Magazine Magazine {
		get {
			return this.magazine;
		}
	}

	private Extension extension;
	public Extension Extension {
		get {
			return this.extension;
		}
	}

	private Accessory accessory;
	public Accessory Accessory {
		get {
			return this.accessory;
		}
	}


	private float shotsPerSecond;
	private float projectileVelocity;
	private float cooldown;

	private bool isAuto {
		get {
			return this.shotsPerSecond == 0.0f;
		}
	}

	private float lastFired;
	private bool isTriggerDown;

	public Propulsor(Barrel barrel, Magazine magazine, Extension extension, Accessory accessory) {
		this.barrel = barrel;
		this.magazine = magazine;
		this.extension = extension;
		this.accessory = accessory;

		this.shotsPerSecond = 0.0f; // TEMP
		this.projectileVelocity = 10.0f; // TEMP

		if (!this.isAuto) {
			this.cooldown = 1.0f / this.shotsPerSecond;
		} else {
			this.cooldown = 0.0f;	
		}

		this.lastFired = 0.0f;
		this.isTriggerDown = false;
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
