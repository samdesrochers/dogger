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

	private float baseVelocity;
	public float BaseVelocity {
		get { 
			return this.baseVelocity;
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

	private float damageModifier;
	public float DamageModifier {
		get {
			return this.damageModifier;
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
	public float Damage { 
		get { 
			if(this.damage == null) {
				this.CalculateDamage();
			}
			return (float)this.damage;
		}
	}

	private float? shotsPerSecond;
	public float ShotsPerSecond { 
		get { 
			if(this.shotsPerSecond == null) {
				this.CalculateShotsPerSecond();
			}
			return (float)this.shotsPerSecond;
		}
	}

	private float? velocity;
	public float Velocity { 
		get { 
			if(this.velocity == null) {
				this.CalculateVelocity();
			}
			return (float)this.velocity;
		}
	}

	private float? cooldown;
	public float Cooldown { 
		get { 
			if(this.cooldown == null) {
				this.CalculateCooldown();
			}
			return (float)this.cooldown;
		}
	}

	private float? totalAmmo;
	public float TotalAmmo { 
		get { 
			if(this.totalAmmo == null) {
				this.CalculateTotalAmmo();
			}
			return (float)this.totalAmmo;
		}
	}

	public Properties(
		float baseDamage = 0f,
		float baseShotsPerSecond = 0f,
		float baseVelocity = 0f,
		float baseCooldown = 0f,
		float baseTotalAmmo = 0f,
		float damageModifier = 0f,
		float shotsPerSecondModifier = 0f,
		float velocityModifier = 0f,
		float cooldownModifier = 0f,
		float totalAmmoModifier = 0f,
		float bonusDamage = 0f)
	{
		this.baseDamage = baseDamage;
		this.baseShotsPerSecond = baseShotsPerSecond;
		this.baseVelocity = baseVelocity;
		this.baseCooldown = baseCooldown;
		this.baseTotalAmmo = baseTotalAmmo;

		this.damageModifier = damageModifier;
		this.shotsPerSecondModifier = shotsPerSecondModifier;
		this.velocityModifier = velocityModifier;
		this.cooldownModifier = cooldownModifier;
		this.totalAmmoModifier = totalAmmoModifier;

		this.bonusDamage = bonusDamage;
	}

	private void CalculateDamage() {
		this.damage = (this.baseDamage + this.bonusDamage) * (1f + this.damageModifier);
	}

	private void CalculateShotsPerSecond() {
		this.shotsPerSecond = this.baseShotsPerSecond * (1f + this.shotsPerSecondModifier);
	}

	private void CalculateVelocity() {
		this.velocity = this.baseVelocity * (1f + this.velocityModifier);
	}

	private void CalculateCooldown() {
		this.cooldown = this.baseCooldown * (1f + this.cooldownModifier);
	}

	private void CalculateTotalAmmo() {
		this.totalAmmo = this.baseTotalAmmo * (1f + this.totalAmmoModifier);
	}

	public static Properties operator +(Properties p1, Properties p2) {
		float _baseDamage = p1.baseDamage + p2.baseDamage;
		float _baseShotsPerSecond = p1.baseShotsPerSecond + p2.baseShotsPerSecond;
		float _baseVelocity = p1.baseVelocity + p2.baseVelocity;
		float _baseCooldown = p1.baseCooldown + p2.baseCooldown;
		float _baseTotalAmmo = p1.baseTotalAmmo + p2.baseTotalAmmo;

		float _shotsPerSecondModifier = p1.shotsPerSecondModifier + p2.shotsPerSecondModifier;
		float _velocityModifier = p1.velocityModifier + p2.velocityModifier;
		float _cooldownModifier = p1.cooldownModifier + p2.cooldownModifier;
		float _damageModifier = p1.damageModifier + p2.damageModifier;
		float _totalAmmoModifier = p1.totalAmmoModifier + p2.totalAmmoModifier;

		float _bonusDamage = p1.bonusDamage + p2.bonusDamage;

		return new Properties(
			baseDamage: _baseDamage,
			baseShotsPerSecond: _baseShotsPerSecond,
			baseVelocity: _baseVelocity,
			baseCooldown: _baseCooldown,
			baseTotalAmmo: _baseTotalAmmo,
			damageModifier: _damageModifier,
			shotsPerSecondModifier: _shotsPerSecondModifier,
			velocityModifier: _velocityModifier,
			cooldownModifier: _cooldownModifier,
			totalAmmoModifier: _totalAmmoModifier,
			bonusDamage: _bonusDamage);
	}
}
