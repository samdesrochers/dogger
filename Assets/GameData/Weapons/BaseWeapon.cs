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

	private bool isTriggerDown;

	public BaseWeapon(Handle handle, PowerModule powerModule, Propulsor[] propulsors) {
		this.fireMode = 0;
		this.handle = handle;
		this.powerModule = powerModule;
		this.propulsors = propulsors;
		this.isTriggerDown = false;
	}

	public virtual bool CanFire() {
		return this.currentPropulsor.CanFire(this.isTriggerDown);
	}

	public virtual void ReleaseTrigger() {
		this.isTriggerDown = false;
	}

	/*public void Fire(Transform weaponTransform, Vector3 target) {
		Vector3 direction = weaponTransform.rotation * Vector3.left;
		Vector2 force = direction * this.projectileForce;
		
		GameObject bulletInstance = (GameObject)MonoBehaviour.Instantiate(this.projectilePrefab, weaponTransform.position, weaponTransform.rotation);
		bulletInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
		bulletInstance.GetComponent<SpriteRenderer>().sortingLayerName = "ProjectilesLayer";
		this.lastFired = Time.time;
		this.isTriggerDown = true;
	}*/
}