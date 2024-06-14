using System.Numerics;
using Raylib_cs;

class Farm
{
	public static void Update()
	{

	}

	public static void Render()
	{
		HighlightTile();
	}



	private static void HighlightTile()
	{
		// Get the mouse position and convert it to world position
		Vector2 mousePosition = Raylib.GetMousePosition();
		Vector2 mouseWorldPosition = Raylib.GetScreenToWorld2D(mousePosition, Player.Camera);

		// Clamp the world position to a tile position
		int tileX = (int)(mouseWorldPosition.X / World.TileSize);
		int tileY = (int)(mouseWorldPosition.Y / World.TileSize);

		// Draw a little black box around the currently highlighted tile
		Raylib.DrawRectangleLines(tileX * World.TileSize, tileY * World.TileSize, World.TileSize, World.TileSize, Color.Black);
	}
}