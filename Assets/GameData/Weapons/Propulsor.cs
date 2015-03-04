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

	private Properties properties;
	public Properties Properties {
		get {
			return this.properties;
		}
	}

	private bool isAuto {
		get {
			return this.properties.ShotsPerSecond != 0.0f;
		}
	}
	
	private float nextFireTime;

	public Propulsor(Barrel barrel, Magazine magazine, Extension extension, Accessory accessory) {
		this.barrel = barrel;
		this.magazine = magazine;
		this.extension = extension;
		this.accessory = accessory;

		this.properties = null; // TEMP
		this.nextFireTime = 0f;
	}

	public bool CanFire(bool isTriggerAlreadyDown) {
		if (this.isAuto) {
			return Time.time > this.nextFireTime;
		} else {
			return !isTriggerAlreadyDown;
		}
	}
}
