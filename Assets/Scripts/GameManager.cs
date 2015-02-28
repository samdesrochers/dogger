using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public GameObject Player { get; private set; }
	
	public List<GameObject> Enemies { get; private set; }
	
	private UnitHealth playerHp;
	private UnitShield playerShield;

	void Start () {
		var map = GameObject.FindWithTag ("Map");
		MapBuilder mapBuilder = map.GetComponent<MapBuilder> ();
		Vector2 spawn = mapBuilder.GetRandomSpawnPoint ();

		this.Player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(spawn.x, spawn.y, 0), new Quaternion(0, 0, 0, 0));
		this.playerHp = Player.GetComponent<UnitHealth> ();
		this.playerShield = Player.GetComponent<UnitShield> ();

		this.Enemies = new List<GameObject>();
		for (int i = 0; i < 10; i++) {
			spawn = mapBuilder.GetRandomSpawnPoint ();
			Enemies.Add ((GameObject)Instantiate (Resources.Load ("Prefabs/viciousCat"), new Vector3 (spawn.x, spawn.y, 0), Quaternion.identity));
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
		Enemies.Remove (unit);
		Destroy (unit);

		if (Enemies.Count == 0) {
			this.pause();
		}
	}

	void Update () {
		if(Input.GetKeyUp("h")) {
			playerShield.TakeHit(25.0f);
		}
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
			posY = playerKvp["posy"],
			ShieldCurrent = playerShield.ShieldValue.ToString(),
			ShieldState = playerShield.State.ToString()
		};
		return ui;
	}
}

public class UIHelper
{
	// Player Attributes
	public string HealthTotal;
	public string HealthCurrent;

	public string ShieldCurrent;
	public string ShieldState;

	public string posX;
	public string posY;
}
