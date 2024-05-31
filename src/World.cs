using System.Numerics;
using Raylib_cs;

class World
{
	private static Vector2 dimensions;
	private static List<Tile> tileDefinitions;

	public static void Start()
	{
		// Open the map file
		string[] mapFile = File.ReadAllLines("./assets/map.txt");
		int mapIndex = 0;

		// Get the dimensions
		dimensions = new Vector2(
			int.Parse(mapFile[mapIndex++]),
			int.Parse(mapFile[mapIndex++])
		);

		// Skip over the separator line
		mapIndex++;

		// Get all of the tile definitions/types so that we can
		// store the actual map as just indexes of the
		// definitions array so its quicker and easier idk
		tileDefinitions = new List<Tile>();
		while (true)
		{
			// Check for if we've reached the end
			// of the tile definition section
			if (mapFile[mapIndex] == "---")
			{
				mapIndex++;
				break;
			}

			// Parse the current tile info
			string[] tileInfo = mapFile[mapIndex].Split(' ');

			// Make the tile
			Tile tile = new Tile()
			{
				Character = tileInfo[0][0],
				Texture = Raylib.LoadTexture($"./assets/texture/{tileInfo[1]}.png"),
				HasCollision = tileInfo[2][0] == 'y'
			};

			// Add the tile to the list of definitions and
			// increase the index for next time
			tileDefinitions.Add(tile);
			mapIndex++;
		}

		// Manually create and add the debug tile
		Tile debugTile = new Tile()
		{
			Character = '?',
			Texture = Raylib.LoadTexture($"./assets/texture/debug.png"),
			HasCollision = false
		};
		tileDefinitions.Add(debugTile);
	}


	struct Tile
	{
		public char Character;
		public Texture2D Texture;
		public bool HasCollision;
	}
}