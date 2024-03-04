using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Raylib_cs;

class Map
{
	public static void LoadMap(string filePath)
	{
		// Open the tiles json file
		string json = File.ReadAllText("./maps/tiles.json");
		Tile[] tiles = JsonSerializer.Deserialize<Tile[]>(json);


		// Open the map file
		string[] mapLines = File.ReadAllLines(filePath);

		
		// Open the map then loop through
		// every tile in the map
		// TODO: Don't use nested foreach
		foreach (string line in mapLines)
		{
			foreach (char identifier in line.ToCharArray())
			{
				// Check for if the 
			}
		}
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
	[JsonPropertyName("identifier")]
	public char Identifier { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("texturePath")]
	public string TexturePath { get; set; }

	[JsonPropertyName("frictionMultiplier")]
	public float FrictionMultiplier { get; set; }

	[JsonPropertyName("collision")]
	public bool Collision { get; set; }
}