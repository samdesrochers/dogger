using System.Collections;

public class Properties {
	private float baseDamage;
	public float BaseDamage {
		get {
			return this.baseDamage;
		}
	}

	private float baseShotsPerSecond;
	public float BaseShotsPerSecond {
		get {
			return this.baseShotsPerSecond;
		}
	}

	private float baseCooldown;
	public float BaseCooldown {
		get {
			return this.baseCooldown;
		}
	}

	private float baseTotalAmmo;
	public float BaseTotalAmmo {
		get {
			return this.baseTotalAmmo;
		}
	}

	private float shotsPerSecondModifier;
	public float ShotsPerSecondModifier {
		get {
			return this.shotsPerSecondModifier;
		}
	}

	private float velocityModifier;
	public float VelocityModifier {
		get {
			return this.velocityModifier;
		}
	}

	private float cooldownModifier;
	public float CooldownModifier {
		get {
			return this.cooldownModifier;
		}
	}

	private float damageModifier;
	public float DamageModifier {
		get {
			return this.damageModifier;
		}
	}

	private float totalAmmoModifier;
	public float TotalAmmoModifier {
		get {
			return this.totalAmmoModifier;
		}
	}

	private float bonusDamage;
	public float BonusDamage {
		get {
			return this.bonusDamage;
		}
	}

	private float? damage;
	public float? Damage { 
		get { 
			if(this.damage == null) {
				this.CalculateDamage();
			}
			return this.damage;
		}
	}

	private float? shotsPerSecond;
	public float? ShotsPerSecond { 
		get { 
			if(this.shotsPerSecond == null) {
				this.CalculateShotsPerSecond();
			}
			return this.shotsPerSecond;
		}
	}

	private float? cooldown;
	public float? Cooldown { 
		get { 
			if(this.cooldown == null) {
				this.CalculateCooldown();
			}
			return this.cooldown;
		}
	}

	private float? totalAmmo;
	public float? TotalAmmo { 
		get { 
			if(this.totalAmmo == null) {
				this.CalculateTotalAmmo();
			}
			return this.totalAmmo;
		}
	}

	public Properties(
		float baseDamage = 0f,
		float baseShotsPerSecond = 0f,
		float baseCooldown = 0f,
		float baseTotalAmmo = 0f,
		float shotsPerSecondModifier = 0f,
		float velocityModifier = 0f,
		float cooldownModifier = 0f,
		float damageModifier = 0f,
		float totalAmmoModifier = 0f,
		float bonusDamage = 0f)
	{
		this.baseDamage = baseDamage;
		this.baseShotsPerSecond = baseShotsPerSecond;
		this.baseCooldown = baseCooldown;
		this.baseTotalAmmo = baseTotalAmmo;

		this.shotsPerSecondModifier = shotsPerSecondModifier;
		this.velocityModifier = velocityModifier;
		this.cooldownModifier = cooldownModifier;
		this.damageModifier = damageModifier;
		this.totalAmmoModifier = totalAmmoModifier;

		this.bonusDamage = bonusDamage;
	}

	private void CalculateDamage() {
		this.damage = (this.baseDamage + this.bonusDamage) * this.damageModifier;
	}

	private void CalculateShotsPerSecond() {
		this.shotsPerSecond = this.baseShotsPerSecond * this.shotsPerSecondModifier;
	}

	private void CalculateCooldown() {
		this.cooldown = this.baseCooldown * this.cooldownModifier;
	}

	private void CalculateTotalAmmo() {
		this.totalAmmo = this.baseTotalAmmo * this.totalAmmoModifier;
	}
}
