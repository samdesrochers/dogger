using UnityEngine;
using System.Collections;

public class MetalMachineGunEmitterController : MonoBehaviour, IProjectileEmitterController {
	private string projectilePrefabPath = "Prefabs/Projectiles/Metal/default";
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// IProjectileEmitterController members
	public void StartEmitting(Properties properties) {
		GameObject projectileContainer = GameObject.Find("Projectiles");
		if (projectileContainer == null) {
			projectileContainer = new GameObject();
			projectileContainer.name = "Projectiles";
		}
		
		Vector3 direction = this.transform.rotation * Vector3.left;
		direction.z = 0;
		direction.Normalize();
		Vector2 velocity = direction * properties.Velocity;
		
		GameObject projectileObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(this.projectilePrefabPath), this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
		projectileObject.name = "MetalMachineGunProjectile";
		projectileObject.AddComponent<ProjectileDefaultController>();

		Transform projectileTransform = projectileObject.GetComponent<Transform>();
		projectileTransform.parent = projectileContainer.GetComponent<Transform>();
		projectileTransform.localScale = new Vector3(EquipmentController.ProjectilesScale, EquipmentController.ProjectilesScale, 1f);
		
		projectileObject.GetComponent<Rigidbody2D>().velocity = velocity;
		projectileObject.GetComponent<SpriteRenderer>().sortingLayerName = "ProjectilesLayer";
	}
	
	public void StopEmitting() {
		// No op for Metal Machine Gun emitter
	}
}
