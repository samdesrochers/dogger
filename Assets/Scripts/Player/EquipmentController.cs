using UnityEngine;
using System.Collections;

public class EquipmentController : MonoBehaviour {
	public const float WeaponPartsScale = 0.25f;
	public const float ProjectilesScale = 0.45f;

	// Use this for initialization
	void Start () {
		this.AssembleWeapons();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private Transform CreateGunContainer(WeaponPosition position) {
		string prefabPath = "Prefabs/Weapons/";
		string objectName = "";

		if (position == WeaponPosition.LEFT) {
			prefabPath += "LeftGunContainer";
			objectName = "LeftGun";
		} else {
			if (PlaythroughManager.Instance.PlayerInfo.Equipment.CurrentWeaponKit == WeaponKit.DUAL_WIELD) {
				prefabPath += "RightGunContainer";
				objectName = "RightGun";
			} else {
				prefabPath += "TwoHandedGunContainer";
				objectName = "RightGun";
			}
		}

		GameObject gunContainerObject = Instantiate(Resources.Load(prefabPath)) as GameObject;
		gunContainerObject.name = objectName;

		Transform gunContainerTransform = gunContainerObject.GetComponent<Transform>();
		Vector3 localPosition = gunContainerTransform.position;
		gunContainerTransform.parent = this.GetComponent<Transform>();
		gunContainerTransform.localPosition = localPosition;

		return gunContainerTransform;
	}

	private void AssembleWeapons() {
		Equipment playerEquipment = PlaythroughManager.Instance.PlayerInfo.Equipment;
		Transform rightGunContainerTransform = this.CreateGunContainer(WeaponPosition.RIGHT);

		if (playerEquipment.CurrentWeaponKit == WeaponKit.DUAL_WIELD) {
			Transform leftGunContainerTransform = this.CreateGunContainer(WeaponPosition.LEFT);
			this.AssembleOneHandedWeapon(leftGunContainerTransform, playerEquipment.LeftGun as OneHandedWeapon);
			this.AssembleOneHandedWeapon(rightGunContainerTransform, playerEquipment.RightGun as OneHandedWeapon);
		} else {
			this.AssembleTwoHandedWeapon(rightGunContainerTransform, playerEquipment.RightGun as TwoHandedWeapon);
		}
	}

	private void AssembleOneHandedWeapon(Transform weaponContainer, OneHandedWeapon weapon) {
		// Draw the Handle
		GameObject handleObject = Instantiate(Resources.Load(weapon.Handle.PrefabFullPath)) as GameObject;
		handleObject.name = "Handle";

		Transform handleTransform = handleObject.GetComponent<Transform>();
		SpriteRenderer handleRenderer = handleObject.GetComponent<SpriteRenderer>();

		handleTransform.parent = weaponContainer;
		handleTransform.localScale = new Vector3(WeaponPartsScale, WeaponPartsScale, 1f);
		float handleTranslateX = handleRenderer.bounds.size.y * -0.5f;
		handleTransform.localPosition = new Vector3(handleTranslateX, 0f, 0f);
		handleTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
		handleRenderer.sortingLayerName = "WeaponsLayer";

		// Draw the Power Module
		GameObject powerModuleObject = Instantiate(Resources.Load(weapon.PowerModule.PrefabFullPath)) as GameObject;
		powerModuleObject.name = "PowerModule";
		
		Transform powerModuleTransform = powerModuleObject.GetComponent<Transform>();
		SpriteRenderer powerModuleRenderer = powerModuleObject.GetComponent<SpriteRenderer>();
		
		powerModuleTransform.parent = weaponContainer;
		powerModuleTransform.localScale = new Vector3(WeaponPartsScale, WeaponPartsScale, 1f);
		float powerModuleTranslateX = -handleRenderer.bounds.size.x - powerModuleRenderer.bounds.size.y * 0.5f;
		powerModuleTransform.localPosition = new Vector3(powerModuleTranslateX, 0f, 0f);
		powerModuleTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
		powerModuleRenderer.sortingLayerName = "WeaponsLayer";

		// Draw the propulsors
		for (int i = 0; i < weapon.Propulsors.Length; i++) {
			Propulsor propulsorToDraw = weapon.Propulsors[i];

			// Create a propulsor container
			GameObject propulsorContainerObject = new GameObject();
			propulsorContainerObject.name = "Propulsor" + i;
			Transform propulsorContainerTransform = propulsorContainerObject.GetComponent<Transform>();
			propulsorContainerTransform.parent = weaponContainer;
			float propulsorTranslateX = -handleRenderer.bounds.size.x - powerModuleRenderer.bounds.size.x;
			float propulsorTranslateY = weapon.PowerModule.BarrelSlots == 1 ? 0f : powerModuleRenderer.bounds.size.y * (0.25f * (-1 + 2 * i));

			propulsorContainerTransform.localPosition = new Vector3(propulsorTranslateX, propulsorTranslateY, 0f);

			// Draw the Barrel
			GameObject barrelObject = Instantiate(Resources.Load(propulsorToDraw.Barrel.PrefabFullPath)) as GameObject;
			barrelObject.name = "Barrel";
			
			Transform barrelTransform = barrelObject.GetComponent<Transform>();
			SpriteRenderer barrelRenderer = barrelObject.GetComponent<SpriteRenderer>();
			
			barrelTransform.parent = propulsorContainerTransform;
			barrelTransform.localScale = new Vector3(WeaponPartsScale, WeaponPartsScale, 1f);
			float barrelTranslateX = -barrelRenderer.bounds.size.y * 0.5f;
			barrelTransform.localPosition = new Vector3(barrelTranslateX, 0f, 0f);
			barrelTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
			barrelRenderer.sortingLayerName = "WeaponsLayer";

			float projectileEmitterTranslateX = -barrelRenderer.bounds.size.x * 0.8f;;

			// Draw the Magazine
			GameObject magazineObject = Instantiate(Resources.Load(propulsorToDraw.Magazine.PrefabFullPath)) as GameObject;
			magazineObject.name = "Magazine";
			
			Transform magazineTransform = magazineObject.GetComponent<Transform>();
			SpriteRenderer magazineRenderer = magazineObject.GetComponent<SpriteRenderer>();
			
			magazineTransform.parent = propulsorContainerTransform;
			magazineTransform.localScale = new Vector3(WeaponPartsScale, WeaponPartsScale, 1f);
			float magazineTranslateX = -magazineRenderer.bounds.size.y * 0.5f;
			magazineTransform.localPosition = new Vector3(magazineTranslateX, 0f, 0f);
			magazineTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
			magazineRenderer.sortingLayerName = "WeaponsLayer";
			magazineRenderer.sortingOrder = 1;

			// Draw the Extension
			if(propulsorToDraw.Extension != null) {
				GameObject extensionObject = Instantiate(Resources.Load(propulsorToDraw.Extension.PrefabFullPath)) as GameObject;
				extensionObject.name = "Extension";
				
				Transform extensionTransform = extensionObject.GetComponent<Transform>();
				SpriteRenderer extensionRenderer = extensionObject.GetComponent<SpriteRenderer>();
				
				extensionTransform.parent = propulsorContainerTransform;
				extensionTransform.localScale = new Vector3(WeaponPartsScale, WeaponPartsScale, 1f);
				float extensionTranslateX = -barrelRenderer.bounds.size.x - extensionRenderer.bounds.size.y * 0.5f;
				extensionTransform.localPosition = new Vector3(extensionTranslateX, 0f, 0f);
				extensionTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
				extensionRenderer.sortingLayerName = "WeaponsLayer";

				projectileEmitterTranslateX = -barrelRenderer.bounds.size.x - extensionRenderer.bounds.size.y * 0.8f;
			}

			// Draw the Accessory
			if(propulsorToDraw.Accessory != null) {
				GameObject accessoryObject = Instantiate(Resources.Load(propulsorToDraw.Accessory.PrefabFullPath)) as GameObject;
				accessoryObject.name = "Accessory";
				
				Transform accessoryTransform = accessoryObject.GetComponent<Transform>();
				SpriteRenderer accessoryRenderer = accessoryObject.GetComponent<SpriteRenderer>();
				
				accessoryTransform.parent = propulsorContainerTransform;
				accessoryTransform.localScale = new Vector3(WeaponPartsScale, WeaponPartsScale, 1f);
				float accessoryTranslateX = -barrelRenderer.bounds.size.x * 0.75f;
				accessoryTransform.localPosition = new Vector3(accessoryTranslateX, 0f, 0f);
				accessoryTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
				accessoryRenderer.sortingLayerName = "WeaponsLayer";
				accessoryRenderer.sortingOrder = 1;
			}

			// Create the projectile emitter
			string emitterPrefab = ProjectileEmitterContainer.GetPrefabPath(propulsorToDraw);
			GameObject emitterObject = Instantiate(Resources.Load(emitterPrefab)) as GameObject;
			emitterObject.name = "ProjectileEmitter";

			Transform emitterTransform = emitterObject.GetComponent<Transform>();
			emitterTransform.parent = propulsorContainerTransform;
			emitterTransform.localPosition = new Vector3(projectileEmitterTranslateX, 0f, 0f);
		}
	}

	private void AssembleTwoHandedWeapon(Transform weaponContainer, TwoHandedWeapon weapon) {
	}
}
