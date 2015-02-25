using System.Collections;

public class Equipment {
	public BaseWeapon LeftGun;
	public BaseWeapon RightGun;

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
