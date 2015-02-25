using System.Collections;

public class BaseWeapon {
	private int fireMode;
	public int FireMode {
		get {
			return this.fireMode;
		}
	}

	private Propulsor currentPropulsor {
		get {
			return this.propulsors[this.fireMode];
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

	public BaseWeapon(Handle handle, PowerModule powerModule, Propulsor[] propulsors) {
		this.fireMode = 0;
		this.handle = handle;
		this.powerModule = powerModule;
		this.propulsors = propulsors;
	}

	public virtual bool CanFire() {
		return this.currentPropulsor.CanFire();
	}

	public virtual void ReleaseTrigger() {
		foreach (Propulsor p in this.propulsors) {
			p.ReleaseTrigger();
		}
	}
}