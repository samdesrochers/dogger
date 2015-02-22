using System.Collections;

public class Equipment {
	public BaseWeapon Weapon1;
	public BaseWeapon Weapon2;

	private static Equipment instance;
	public static Equipment Instance {
		get {
			if (instance == null) {
				instance = new Equipment();
			}
			
			return instance;
		}
	}
	
	private Equipment() {
	}
}
