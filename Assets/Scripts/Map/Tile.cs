using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	public bool IsEnabled;
	public bool Flagged;
	public Vector2 Position;
	public List<Vector2> Adjacents;
	public Tile(Vector2 pos)
	{
		Flagged = false;
		IsEnabled = true;
		Position = pos;

		Adjacents = new List<Vector2> ();
		Adjacents.Add (Position + new Vector2 (1, 0));
		Adjacents.Add (Position + new Vector2 (-1, 0));
		Adjacents.Add (Position + new Vector2 (0, 1));
		Adjacents.Add (Position + new Vector2 (0, -1));
	}
}


