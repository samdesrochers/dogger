using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapBuilder : MonoBehaviour {

	public Dictionary<string, Tile> TilePositions = new Dictionary<string, Tile>();
	public Dictionary<string, Tile> BorderPositions = new Dictionary<string, Tile>();

	public Vector2 InitialPosition;
	public int DesiredSize;

	private Object tilePrefab;
	private Object borderPrefab;

	private const int randomSections = 15;

	void Start () {

		tilePrefab = Resources.Load("Prefabs/MapObjects/Ground");
		borderPrefab = Resources.Load("Prefabs/MapObjects/Border");

		//GenreateRandomDungeon ();

		//10, 9, 5 (huge)
		//8, 9, 3 (big)
		//6, 5, 1 (medium)
		//5, 4, 1 (small)
		GenerateGridArena (5, 4, 1);

		GenerateDungeonBorder ();
		FillWorld ();
	}

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
				if (!TilePositions.ContainsKey (PositionToKey (t.Position))) {
					TilePositions.Add (PositionToKey (t.Position), t);
				}
			}
		}
	}

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
			if(!TilePositions.ContainsKey(key))
			{
				TilePositions.Add(key, t);
			}
		}

		remainingDepth--;
		Tile nextEdge = GetRandomEdge (section);

		if(nextEdge != null)
			GenerateRandomDungeon (nextEdge.Position, remainingDepth);
	}

	Tile GetRandomEdge(List<Tile> lastSection)
	{
		int edgeCount = lastSection.Count - 1;
		while (edgeCount > 0) {	
			int rand = Random.Range(0, edgeCount);

			Tile t = lastSection[rand];
			lastSection.Remove(t);
		
			int count = 1;
			foreach(var pos in t.Adjacents) 
				if(TilePositions.ContainsKey(PositionToKey(pos)))
					count++;

			// Retrurn first edge that doesn't have 4 adjacent tiles
			if(count < 4)
				return t;

			edgeCount--;
		}

		return null;
	}

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
		int rand = Random.Range (0, TilePositions.Count - 1);
		Tile t = TilePositions.ElementAt(rand).Value;

		Vector2 seed = t.Position;
		GenerateRandomDungeon(seed, randomSize);
	}

	void GenerateDungeonBorder() 
	{
		// Initial construction of all basic borders
		foreach (Tile t in TilePositions.Values) 
		{
			foreach(var adjacent in t.Adjacents)
			{
				string edgeKey = PositionToKey(adjacent);
				if(!TilePositions.ContainsKey(edgeKey))
				{
					if(!BorderPositions.ContainsKey(edgeKey))
					{
						Tile border = new Tile(adjacent);
						BorderPositions.Add(edgeKey, border);
					}
				}
			}
		}

//		List<Tile> additionalBorders = new List<Tile>();
//
//		// Fill corners
//		foreach (var border in BorderPositions) 
//		{
//			string grandParentKey = PositionToKey(border.Value.Position);
//			foreach(var adj in border.Value.Adjacents)
//			{
//				Tile temp = new Tile(adj);
//				string parentKey = PositionToKey(adj);
//
//				foreach(var tempAdjPos in temp.Adjacents)
//				{
//					string key = PositionToKey(tempAdjPos);
//
//					if(key.Equals(parentKey) || key.Equals(grandParentKey))
//						continue;
//					else if(TilePositions.ContainsKey(key))
//						continue;
//					else if (BorderPositions.ContainsKey(key))
//						continue;
//
//					Tile newBorder = new Tile(tempAdjPos);
//					additionalBorders.Add(newBorder);
//				}
//			}
//		}
//
//		foreach (Tile t in additionalBorders) {
//			BorderPositions.Add(PositionToKey(t.Position), t);
//		}
	}

	void FillWorld()
	{
		foreach (var pos in TilePositions.Values) 
			AddGroundTile(pos.Position);

		foreach (var pos in BorderPositions.Values) 
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

	string PositionToKey(Vector2 pos)
	{
		return "x"+pos.x.ToString()+"y"+pos.y.ToString();
	}
}