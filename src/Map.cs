using Raylib_cs;

class Map
{
	public static List<Texture2D> Foreground;
	public static List<Texture2D> Midground;
	public static List<Texture2D> Background;

	private static List<Texture2D> textures;
	private static int mapWidth = 0;

	public static void LoadMap(string filePath)
	{
		// Store all of the different layers of the map
		Foreground = new List<Texture2D>();
		Midground = new List<Texture2D>();
		Background = new List<Texture2D>();

		// Open the map file and get the contents
		string[] mapData = File.ReadAllLines(filePath);

		// Store all of the tiles
		Dictionary<int, Texture2D> tiles = new Dictionary<int, Texture2D>();
		textures = new List<Texture2D>();

		// Loop over every line to get its data
		foreach (string line in mapData)
		{
			// Check for if the current line has a
			// comment and get rid of it
			if (line.StartsWith('#')) continue;
			//! if (line.Contains('#')) line = line.Split('#')[0];

			// Check for if it's defining a tile
			if (line.Contains(':'))
			{
				// Split the line to get the key, and
				// the texture location
				string[] tileInfo = line.Split(":");
				int key = int.Parse(tileInfo[0]);
				string texturePath = tileInfo[1];

				// TODO: Check for if there is no texture, and skip it

				// Load in the texture
				// ! this might be loading in the texture twice because of the dictionary but idk
				Texture2D texture = Raylib.LoadTexture(texturePath);
				textures.Add(texture);

				// Add the tile to the list of tiles
				tiles.Add(key, texture);
				continue;
			}

			// See if the map width has grown
			int currentWidth = line.Length;
			if (mapWidth > currentWidth) mapWidth = currentWidth;

			// Split each individual tile in the
			// current row of the map
			foreach (string tileIdentifier in line.Split(""))
			{
				// Get the corresponding texture to the tile thingy
				// TODO: Make tile dict use char instead of int
				// TODO: Error handling for unknown tile identifier thing
				Console.WriteLine(tileIdentifier);
				// Texture2D tile = tiles[tileIdentifier];

				// Add the tile to the map
				// TODO: Control wether we add to the foreground, midground, or background
				// Midground.Add(tile);
			}
		}
	}

	public static void Render()
	{
		
	}

	public static void CleanUp()
	{
		// Unload all the textures
		foreach (Texture2D texture in textures)
		{
			Raylib.UnloadTexture(texture);
		}
	}
}