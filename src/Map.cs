using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Raylib_cs;

class Map
{	
	public static List<Tile> Tiles = new List<Tile>();
	public static readonly int TileSize = 16; //? Could be stored as vector
	public static int Width = 0;

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
			if (line.Length > Width) Width = line.Length;

			// Loop through every tile in the line
			foreach (char tileIdentifier in line)
			{
				// Check for if there is a tile with the
				// same identifier
				Tile tile = tiles.FirstOrDefault(tile => tile.Identifier == tileIdentifier);
				if (tile == null) continue;

				// Add the tile to the map
				Tiles.Add(tile);
			}
		}
	}



	public static void Render()
	{
		// Loop over every tile in the map and render it
		// TODO: Split up the map into 'chunks' and bake them
		for (int i = 0; i < Tiles.Count; i++)
		{
			// Check for if the current tile has a texture
			if (Tiles[i].Texture == null) continue;

			// Get the coordinates of the current tile
			Vector2 tilePosition = new Vector2(i % Width, i / Width) * TileSize;

			// Check for if the tile is within a certain
			// distance of the player then draw it
			if (Utils.Distance(Player.Position, tilePosition) > (Game.GameWidth / 2)) return;
			Raylib.DrawTextureV((Texture2D)Tiles[i].Texture, tilePosition, Color.White);
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