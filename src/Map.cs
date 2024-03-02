using Raylib_cs;

class Map
{
	private static Dictionary<char, Texture2D?> tiles = new Dictionary<char, Texture2D?>();
	private static Texture2D mapTexture;

	public static void LoadMap(string filePath)
	{
		// Open the map file
		string[] mapData = File.ReadAllLines(filePath);


		// Store the identifier, and the texture of a tile
		tiles = new Dictionary<char, Texture2D?>();
		int width = 0;

		// Store the tiles in the map as individual textures
		List<Texture2D?> map = new List<Texture2D?>();

		// Loop through every line in the
		// file and extract it's information
		foreach (string line in mapData)
		{
			// Ignore the current line if there is a comment
			// TODO: Also support inline comments
			if (line.StartsWith("#")) continue;

			// Check for if we're defining a tile, or
			// if we're showing the map
			//? Not using guard clause here because it's better for readability
			if (line.Contains(":"))
			{
				// Split the tile into an identifier, and texture
				string[] data = line.Trim().Split(":");
				char identifier = data[0].Trim()[0];
				string texturePath = data[1];

				// Load in the texture if a
				// texture path was supplied
				Texture2D? texture = null;
				if (texturePath != "") texture = Raylib.LoadTexture(texturePath);

				// Add the tile to the list of tiles
				tiles.Add(identifier, texture);
				Console.WriteLine($"Loaded '{identifier}' tile as {texturePath}!!");
			}
			else
			{
				// Check for if the map width has been extended
				if (line.Length > width) width = line.Length;

				// Get the current line and split it up into
				// each character/tile for iterating over
				foreach (char tileIdentifier in line.ToCharArray())
				{
					// Check for if a tile with the identifier exists
					if (!tiles.ContainsKey(tileIdentifier)) continue;

					// Add the tile to the map
					// TODO: Could store chars instead of textures. Might be faster
					map.Add(tiles[tileIdentifier].Value);
				}
			}

			// Bake the map into a single texture
			// to reduce draw calls
			
		}
	}



	public static void CleanUp()
	{
		// Unload all of the tiles
		foreach (KeyValuePair<char, Texture2D?> tile in tiles)
		{
			if (tile.Value == null) continue;
			Raylib.UnloadTexture((Texture2D)tile.Value);
		}
	}



	private static Texture2D BakeMap(List<Texture2D?> tiles, int width)
	{
		return 	new Texture2D()  ;
	}
}