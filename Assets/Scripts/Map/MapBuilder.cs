using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapBuilder : MonoBehaviour {

	public Dictionary<string, Tile> TilePositions = new Dictionary<string, Tile>();
	public Dictionary<string, Tile> BorderPositions = new Dictionary<string, Tile>();

	public Vector2 InitialPosition;
	public int DesiredSize;

	private Object tilePrefab;
	private Object borderPrefab;

	void Start () {

		tilePrefab = Resources.Load("Prefabs/MapObjects/Ground");
		borderPrefab = Resources.Load("Prefabs/MapObjects/Border");

		GenreateDungeon ();
	}

	void GenreateDungeon()
	{
		GenerateRandomDungeon(InitialPosition, DesiredSize);
		GenerateDungeonBorder ();
		FillWorld ();
	}

	void GenerateRandomDungeon(Vector2 initialPosition, int remainingDepth)
	{
		if (remainingDepth == 0)
			return;

		int randSizeDecider = (Random.Range(0,100) < 60) ? 5 : 15;
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
		GenerateRandomDungeon (nextEdge.Position, remainingDepth);
	}

	Tile GetRandomEdge(List<Tile> lastSection)
	{
		while (true) {	
			int rand = Random.Range(0,lastSection.Count - 1);

			Tile t = lastSection[rand];
			lastSection.Remove(t);
		
			int count = 1;
			foreach(var pos in t.Adjacents) 
				if(TilePositions.ContainsKey(PositionToKey(pos)))
					count++;

			// Retrurn first edge that doesn't have 4 adjacent tiles
			if(count < 4)
				return t;
		}
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

	void GenerateDungeonBorder() 
	{
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
		groundTile.transform.parent = transform.FindChild("StartingPoint");
	}

	void AddBorderTile(Vector2 position){
		GameObject borderTile = (GameObject)Instantiate(this.borderPrefab, this.transform.position, Quaternion.identity);

		borderTile.transform.position = position;
		borderTile.transform.parent = transform.FindChild("StartingPoint");
	}

	string PositionToKey(Vector2 pos)
	{
		return pos.x.ToString()+pos.y.ToString();
	}
}