using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	
	private List<GameObject> enemies;

	private UnitHealth playerHp;

	void Start () {
		//Instanciate the player - to do
		//this.player = (GameObject)Instantiate(Resources.Load("Prefabs/player"), new Vector3(2,0,0), new Quaternion(0, 0, 0, 0));

		playerHp = Player.GetComponent<UnitHealth> ();

		//Create the ennemies - to do keyvohn, generate from the map tile
		enemies = new List<GameObject>();
		enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
	}
	
	void Update () {
		Destroy (null);
	}

	Dictionary<string, string> GetPlayerKvp ()
	{
		Dictionary<string, string> kvp = new Dictionary<string, string> ();

		kvp.Add ("hp", playerHp.health.ToString ());
		kvp.Add ("posx", Player.transform.position.x.ToString ("N0"));
		kvp.Add ("posy", Player.transform.position.y.ToString("N0"));
		return kvp;
	}

	public UIHelper GetUIFrame()
	{
		Dictionary<string, string> playerKvp = GetPlayerKvp ();
		UIHelper ui = new UIHelper {
			HealthTotal = "100",
			HealthCurrent = playerKvp["hp"],
			posX = playerKvp["posx"],
			posY = playerKvp["posy"]
		};
		return ui;
	}
}

public class UIHelper
{
	// Player Attributes
	public string HealthTotal;
	public string HealthCurrent;
	public string posX;
	public string posY;
}
