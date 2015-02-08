using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapBuilder : MonoBehaviour {

	/// <summary>
	/// The map tiles.
	/// </summary>
	public Dictionary<string, Tile> MapTiles = new Dictionary<string, Tile>();

	/// <summary>
	/// The map borders.
	/// </summary>
	public Dictionary<string, Tile> MapBorders = new Dictionary<string, Tile>();

	public Vector2 InitialPosition;
	public int DesiredSize;

	private Object tilePrefab;
	private Object borderPrefab;

	private int randomSections = 15;

	void Start () {

		tilePrefab = Resources.Load("Prefabs/MapObjects/Ground");
		borderPrefab = Resources.Load("Prefabs/MapObjects/Border");

		GenreateRandomDungeon ();

		//10, 9, 5 (huge)
		//8, 9, 3 (big)
		//6, 5, 1 (medium)
		//5, 4, 1 (small)
		//GenerateGridArena (5, 4, 1);

		GenerateDungeonBorder ();
		FillWorld ();
	}

	// Generates a SQAURE, "Column" separated map
	void GenerateGridArena(int mapSize, int sectionSize, int spacer)
	{
		List<List<Tile>> sections = new List<List<Tile>> ();

		for (int i = 0; i < mapSize; i++) 
		{
			for (int j = 0; j < mapSize + spacer; j++) 
			{
				int nextX = (sectionSize + spacer) * i;
				int nextY = (sectionSize) * j;
				Vector2 nextPos = new Vector2 (nextX, nextY);
				List<Tile> section = GetNxNSection (nextPos, sectionSize);
				sections.Add(section);
			}	
		}

		for (int i = 0; i < mapSize + spacer; i++) 
		{
			for (int j = 0; j < mapSize; j++) 
			{
				int nextX = (sectionSize) * i;
				int nextY = (sectionSize + spacer) * j;
				Vector2 nextPos = new Vector2 (nextX, nextY);
				List<Tile> section = GetNxNSection (nextPos, sectionSize);
				sections.Add(section);
			}	
		}

		foreach (List<Tile> tiles in sections) {
			foreach (Tile t in tiles) {
				if (!MapTiles.ContainsKey (PositionToKey (t.Position))) {
					MapTiles.Add (PositionToKey (t.Position), t);
				}
			}
		}
	}

	// Generate a completely random dungeon
	void GenreateRandomDungeon()
	{
		GenerateRandomDungeon(InitialPosition, DesiredSize);
		for (int i = 0; i < randomSections; i++) {
			AddRandomSections ();
		}
	}

	void GenerateRandomDungeon(Vector2 initialPosition, int remainingDepth)
	{
		if (remainingDepth == 0)
			return;

		int randSizeDecider = Random.Range(3,20);
		List<Tile> section = GetNxNSection (initialPosition, randSizeDecider);
		foreach (var t in section) {
			string key = PositionToKey(t.Position);
			if(!MapTiles.ContainsKey(key))
			{
				MapTiles.Add(key, t);
			}
		}

		remainingDepth--;
		Tile nextEdge = GetRandomEdge (section);

		if(nextEdge != null)
			GenerateRandomDungeon (nextEdge.Position, remainingDepth);
	}

	// Picks an edge at random, returns null if none found
	// EDGE : tile that has at least 1 non-ground adjacent tile
	Tile GetRandomEdge(List<Tile> lastSection)
	{
		int edgeCount = lastSection.Count - 1;
		while (edgeCount > 0) {	
			int rand = Random.Range(0, edgeCount);

			Tile t = lastSection[rand];
			lastSection.Remove(t);
		
			int count = 1;
			foreach(var pos in t.Adjacents) 
				if(MapTiles.ContainsKey(PositionToKey(pos)))
					count++;

			// Retrurn first edge that doesn't have 4 adjacent tiles
			if(count < 4)
				return t;

			edgeCount--;
		}

		return null;
	}

	// Returns a ground square section of size N. 
	// Should only use odd size number for best results
	// Origin is always at the very middle of the NxN section
	// ex : origin = 1,1   size = 3
	//	
	//	returns :	[0,2] [1,2] [2,2]	
	//				[0,1] [1,1] [2,1]
	//				[0,0] [1,0] [2,0] 
	// As a List of Tiles
	List<Tile> GetNxNSection(Vector2 origin, int size)
	{
		List<Tile> tiles = new List<Tile> ();
		Tile center = new Tile(origin);
		int delimiter = size/2;

		center.Flagged = true;
		tiles.Add (center);

		for (int i = 0; i < size; i++) 
		{
			for(int j = size - 1; j >= 0; j--)
			{
				Vector2 newPos = new Vector2(origin.x - delimiter + i, origin.y - delimiter + j); 
				tiles.Add (new Tile (newPos));
			}		
		}

		return tiles;
	}
	
	void AddRandomSections()
	{
		int randomSize = Random.Range (2, 30);
		int rand = Random.Range (0, MapTiles.Count - 1);
		Tile t = MapTiles.ElementAt(rand).Value;

		Vector2 seed = t.Position;
		GenerateRandomDungeon(seed, randomSize);
	}

	// Generates the borders of the dungeon once all the ground 
	// has been set up in the MapTiles List
	void GenerateDungeonBorder() 
	{
		// Initial construction of all basic borders
		foreach (Tile t in MapTiles.Values) 
		{
			foreach(var adjacent in t.Adjacents)
			{
				string edgeKey = PositionToKey(adjacent);
				if(!MapTiles.ContainsKey(edgeKey))
				{
					if(!MapBorders.ContainsKey(edgeKey))
					{
						Tile border = new Tile(adjacent);
						MapBorders.Add(edgeKey, border);
					}
				}
			}
		}
	}

	// Adds prefab for both ground and border positions
	void FillWorld()
	{
		foreach (var pos in MapTiles.Values) 
			AddGroundTile(pos.Position);

		foreach (var pos in MapBorders.Values) 
			AddBorderTile(pos.Position);	
	}

	void AddGroundTile(Vector2 position)
	{
		GameObject groundTile = (GameObject)Instantiate(this.tilePrefab, this.transform.position, Quaternion.identity);
		groundTile.transform.position = position;
		groundTile.transform.parent = transform.Find("Map");
	}

	void AddBorderTile(Vector2 position){
		GameObject borderTile = (GameObject)Instantiate(this.borderPrefab, this.transform.position, Quaternion.identity);

		borderTile.transform.position = position;
		borderTile.transform.parent = transform.FindChild("Map");
	}

	// Util key convert fo unified values in different dictionaries
	string PositionToKey(Vector2 pos)
	{
		return "x"+pos.x.ToString()+"y"+pos.y.ToString();
	}
}