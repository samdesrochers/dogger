public class PlayerInfo {
	private Equipment equipment;
	public Equipment Equipment { 
		get {
			return this.equipment;
		} 
	}

	private Inventory inventory;
	public Inventory Inventory {
		get {
			return this.inventory;
		}
	}

	public PlayerInfo(WeaponKit weaponKit) {
		this.equipment = new Equipment(weaponKit);
		this.inventory = new Inventory();
	}
}
