using System.Collections;

public class BaseWeapon {
	private int fireMode;
	public int FireMode {
		get {
			return this.fireMode;
		}
	}

	public Propulsor ActivePropulsor {
		get {
			return this.propulsors[this.fireMode];
		}
	}

	public string ActivePropulsorGameObjectName {
		get {
			return "Propulsor" + this.fireMode;
		}
	}

	private Handle handle;
	public Handle Handle {
		get {
			return this.handle;
		}
	}
	
	private PowerModule powerModule;
	public PowerModule PowerModule {
		get {
			return this.powerModule;
		}
	}


	private Propulsor[] propulsors;
	public Propulsor[] Propulsors {
		get {
			return this.propulsors;
		}
	}

	private bool isTriggerDown;

	public BaseWeapon(Handle handle, PowerModule powerModule, Propulsor[] propulsors) {
		this.fireMode = 0;
		this.handle = handle;
		this.powerModule = powerModule;
		this.propulsors = propulsors;
		this.isTriggerDown = false;

		this.CombineProperties();
	}

	private void CombineProperties(){
		Properties aggregatedProperties = this.handle.Properties + this.powerModule.Properties;
		foreach (Propulsor p in this.propulsors) {
			p.InitializeProperties(aggregatedProperties);
		}
	}

	public bool CanFire() {
		return this.ActivePropulsor.CanFire(this.isTriggerDown);
	}

	public void TriggerReleased() {
		this.isTriggerDown = false;
	}

	public void WeaponFired() {
		this.isTriggerDown = true;
		this.ActivePropulsor.WeaponFired();
	}
}