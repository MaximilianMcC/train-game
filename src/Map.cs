using System.Numerics;
using Raylib_cs;

class Map
{
	public static void LoadMap(string filePath)
	{
		// Load in all of the tiles
	}



	public static void Render()
	{

	}

	public static void CleanUp()
	{

	}
}


class Tile
{
	public char Identifier { get; set; }
	public string Name { get; set; }
	public string TexturePath { get; set; }
	public float FrictionMultiplier { get; set; }
	public bool Collision { get; set; }
}