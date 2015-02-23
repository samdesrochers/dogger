using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject Player;

	private List<GameObject> enemies;

	private UnitHealth playerHp;

	void Start () {
		//Instanciate the player - to do
		this.Player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(2,0,0), new Quaternion(0, 0, 0, 0));

		playerHp = Player.GetComponent<UnitHealth> ();

		//Create the ennemies - to do keyvohn, generate from the map tile
		enemies = new List<GameObject>();
		for (int i=0; i < 2; ++i)
		{
			GameObject enemyToAdd = (GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity);
			enemies.Add (enemyToAdd);
		}
	}

	private void pause()
	{
		Time.timeScale = 0.0f;
	}

	// To do keyvohn - put this (as well as the stuff that is in start) in a unit manager
	public void unitDied (GameObject unit)
	{
		if (unit.tag == "Player") {
			this.pause();
			return;
		}

		// Remove the enemy from the dict
		enemies.Remove (unit);
		Destroy (unit);

		if (enemies.Count == 0) {
			this.pause();
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
