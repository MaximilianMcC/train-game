using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Raylib_cs;

class Map
{	
	private static List<Texture2D?> map = new List<Texture2D?>();
	private static int width = 0;
	private static readonly int tileSize = 16;

	public static void LoadMap(string filePath)
	{
		// Open the tiles json file
		string json = File.ReadAllText("./maps/tiles.json");
		Tile[] tiles = JsonSerializer.Deserialize<Tile[]>(json);

		// Load in all of the textures
		foreach (Tile tile in tiles)
		{
			// Don't load it if the texture has already been loaded,
			// or if there is no texture path supplied
			if (tile.Texture != null || tile.TexturePath == null) continue;

			// Actually load the texture
			tile.Texture = Raylib.LoadTexture(tile.TexturePath);
		}

		// Open the map and convert it to textures
		string[] mapLines = File.ReadAllLines(filePath);
		foreach (string line in mapLines)
		{
			// Check for if the width has increased
			if (line.Length > width) width = line.Length;

			// Loop through every tile in the line
			foreach (char tileIdentifier in line)
			{
				// Check for if there is a tile with the
				// same identifier
				Tile tile = tiles.FirstOrDefault(tile => tile.Identifier == tileIdentifier);
				if (tile == null) continue;

				// Add the tile to the map
				map.Add(tile.Texture);
			}
		}
	}



	public static void Render()
	{
		// Loop over every tile in the map and render it
		// TODO: Only render every tile on screen
		// TODO: Split up the map into 'chunks' and bake them
		for (int i = 0; i < map.Count; i++)
		{
			// Check for if the current tile has a texture
			if (map[i] == null) continue;

			// Get the coordinates of the current tile
			int x = i % width * tileSize;
			int y = i / width * tileSize;

			// Draw the tile
			// TODO: Check for if the tile is within a certain distance of the player
			Raylib.DrawTexture((Texture2D)map[i], x, y, Color.White);
		}
	}

	public static void CleanUp()
	{
		// TODO: Unload all of the textures
	}




}


class Tile
{
	[JsonPropertyName("identifier")]
	public char Identifier { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("frictionMultiplier")]
	public float FrictionMultiplier { get; set; }

	[JsonPropertyName("collision")]
	public bool Collision { get; set; }

	[JsonPropertyName("texturePath")]
	public string TexturePath { get; set; }
	public Texture2D? Texture { get; set; }
}