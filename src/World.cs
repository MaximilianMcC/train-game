using System.Numerics;
using Raylib_cs;

class World
{
	public static int Width;
	public static int Height;
	private static List<Tile> tileDefinitions;
	private static Tile[] map;

	// TODO: Put in map file
	public static int TileSize = 16;

	public struct Tile
	{
		public char Character;
		public Texture2D Texture;
		public bool HasCollision;
	}

	// Load the map
	public static void Start()
	{
		// Open the map file
		string[] mapFile = File.ReadAllLines("./assets/map.txt");
		int mapIndex = 0;

		// Get the dimensions
		Width = int.Parse(mapFile[mapIndex++]);
		Height = int.Parse(mapFile[mapIndex++]);

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

		// Make an array the same size as the map
		// and store all of the tiles in it
		map = new Tile[Width * Height];

		// Loop through every character in the map and
		// get its corresponding tile, then add that
		// tile to the previously created tiles array
		int index = 0;
		for (int y = mapIndex; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				// Get the current character
				char character = mapFile[y][x];

				// Check for if the current character has
				// a corresponding tile thats been defined,
				// otherwise just use a debug tile
				Tile currentTile = debugTile;
				foreach (Tile tile in tileDefinitions)
				{
					if (tile.Character == character)
					{
						// Set the current tile to the
						// correct tile, then exit the 
						// loop early (already done)
						currentTile = tile;
						break;
					}
				}

				// Add the tile to the array
				map[index] = currentTile;
				index++;
			}
		}
	}
	
	// Draw the map
	// TODO: Maybe bake the map into bigger chunks
	public static void Render()
	{
		// Loop over every tile in the map
		for (int i = 0; i < map.Length; i++)
		{
			// Get its position
			int x = (i % Width) * TileSize;
			int y = (i / Width) * TileSize;

			// Actually draw it
			Raylib.DrawTexture(map[i].Texture, x, y, Color.White);
		}
	}

	public static void CleanUp()
	{
		// Loop through every tile and unload the texture
		tileDefinitions.ForEach(tile => Raylib.UnloadTexture(tile.Texture));
	}


	// Check for if the player is colliding with a tile
	// that hasn't got collision
	public static bool Colliding(Rectangle newBody)
	{
		// Loop through every tile in the map
		// TODO: Only check the tiles surrounding the player
		for (int i = 0; i < map.Length; i++)
		{
			// Ignore the tile if it hasn't got collision
			if (map[i].HasCollision == false) continue;

			// Calculate the position and whatnot
			// TODO: Put in the struct so all this stuff hasn't got to be calculated every frame
			Rectangle tileRectangle = new Rectangle(
				(i % Width) * TileSize,
				(i / Width) * TileSize,
				TileSize, TileSize
			);
			
			// Actually do the collision detection
			if (Raylib.CheckCollisionRecs(tileRectangle, newBody)) return true;
		}

		// There were no collisions
		return false;
	}
}