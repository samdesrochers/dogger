using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private GameObject player;
	
	private List<GameObject> ennemies;

	// Use this for initialization
	void Start () {
		//Instanciate the player - to do
		//this.player = (GameObject)Instantiate(Resources.Load("Prefabs/player"), new Vector3(2,0,0), new Quaternion(0, 0, 0, 0));

		//Create the ennemies - to do keyvohn, generate from the map tile
		ennemies = new List<GameObject>();
		ennemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		ennemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		ennemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		ennemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		ennemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
		ennemies.Add ((GameObject)Instantiate(Resources.Load("Prefabs/viciousCat"), new Vector3(2,0,0), Quaternion.identity));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
