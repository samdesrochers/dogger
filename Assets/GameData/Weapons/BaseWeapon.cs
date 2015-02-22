using System.Collections;

public class BaseWeapon {
	public Handle handle;
	public PowerModule powerModule;
	public Propulsor primaryPropulsor;

	public BaseWeapon(Handle handle, PowerModule powerModule, Propulsor primaryPropulsor) {
		this.handle = handle;
		this.powerModule = powerModule;
		this.primaryPropulsor = primaryPropulsor;
	}

	public virtual bool CanFire() {
		return this.primaryPropulsor.CanFire();
	}

	public virtual void ReleaseTrigger() {
		this.primaryPropulsor.ReleaseTrigger();
	}
}