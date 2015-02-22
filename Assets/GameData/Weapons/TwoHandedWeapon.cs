using System;

public class TwoHandedWeapon : BaseWeapon {
	private enum FireMode {PRIMARY, SECONDARY};

	public readonly float spriteScaleX = 1.1f;
	public readonly float spriteScaleY = 1.2f;

	private Propulsor secondaryPropulsor;
	private FireMode currentFireMode;

	private Propulsor currentPropulsor {
		get {
			return this.currentFireMode == FireMode.PRIMARY ? this.primaryPropulsor : this.secondaryPropulsor;
		}
	}

	public TwoHandedWeapon(Handle handle, PowerModule powerModule, Propulsor primaryPropulsor, Propulsor secondaryPropulsor) : base(handle, powerModule, primaryPropulsor) {
		this.secondaryPropulsor = secondaryPropulsor;
		this.currentFireMode = FireMode.PRIMARY;
	}

	public override bool CanFire() {
		return this.currentPropulsor.CanFire ();
	}

	public override void ReleaseTrigger() {
		this.primaryPropulsor.ReleaseTrigger();
		this.secondaryPropulsor.ReleaseTrigger();
	}
}
