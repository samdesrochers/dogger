using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject Player { get; private set; }
	
	public List<GameObject> Enemies { get; private set; }

	private UnitHealth playerHp;

	void Start () {
				
		var map = GameObject.FindWithTag ("Map");
		MapBuilder mapBuilder = map.GetComponent<MapBuilder> ();
		Vector2 spawn = mapBuilder.GetRandomSpawnPoint ();

		//Instanciate the player - to do
		this.Player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(spawn.x, spawn.y, 0), new Quaternion(0, 0, 0, 0));
		this.playerHp = Player.GetComponent<UnitHealth> ();

		//Create the ennemies - to do keyvohn, generate from the map tile
		this.Enemies = new List<GameObject>();
		for (int i = 0; i < 300; i++)
		{
			spawn = mapBuilder.GetRandomSpawnPoint ();
			Enemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(spawn.x, spawn.y, 0), Quaternion.identity));
		}
	}
	
	void Update () {

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
