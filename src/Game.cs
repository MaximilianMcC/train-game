using System.Numerics;
using Raylib_cs;

class Game
{
	public static Vector2 Size = new Vector2(800, 600);
	private static RenderTexture2D cameraOutput;

	public static void Run()
	{
		// Raylib stuff
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow((int)Size.X, (int)Size.Y, "farmer (farming rn)");
		Raylib.SetTargetFPS(144);
		Raylib.SetExitKey(KeyboardKey.Null);

		// Main game loop
		Start();
		while (!Raylib.WindowShouldClose())
		{
			// Update everything
			Update();

			// Draw everything to the output render texture
			Raylib.BeginDrawing();
			Raylib.BeginTextureMode(cameraOutput);
			Raylib.ClearBackground(Color.Magenta);
			Render();
			Raylib.EndTextureMode();

			// Draw the output render texture in the centre of the screen
			// With black bars on the side (4:3 aspect rn)
			DrawGameOutput();
			Raylib.EndDrawing();
		}
		CleanUp();
	}

	private static void Start()
	{
		// Make the render texture so that we can
		// scale the game up/down and whatnot
		cameraOutput = Raylib.LoadRenderTexture((int)Size.X, (int)Size.Y);

		Player.Start();
		World.Start();
	}

	private static void Update()
	{
		Player.Update();
		GameManager.Update();
		Farm.Update();
	}

	private static void Render()
	{
		// Draw world stuff (follows the player in the world)
		Raylib.BeginMode2D(Player.Camera);
		World.Render();
		Farm.Render();
		Player.Render();
		// GameManager.DrawTimeOverlay();
		Raylib.EndMode2D();

		// Draw UI stuff (hasn't got a world position)
		Raylib.DrawText("im farm", 10, 10, 30, Color.White);
		GameManager.Render();
	}

	private static void CleanUp()
	{
		World.CleanUp();
		Player.CleanUp();
		Raylib.UnloadRenderTexture(cameraOutput);

		// Kill all the raylib stuff
		//! Make sure this is done last
		Raylib.CloseWindow();
	}


	private static void DrawGameOutput()
	{
		// Get the section of the texture to take
		//? Negative height because openGl draws upside down (rinky)
		Rectangle source = new Rectangle(Vector2.Zero, Size.X, -Size.Y);

		// Calculate the scale factors for if we want the output
		// to be scaled so that the height is fully used, then
		// we have bars on the sides. idk if that makes any sense
		//? scale is based off the Y btw
		float scale = Raylib.GetScreenHeight() / Size.Y;
		Vector2 newSize = new Vector2(scale) * Size;

		// Get the x position to move the output into
		// the centre of the screen
		float x = (Raylib.GetScreenWidth() - newSize.X) / 2;

		// Draw the texture
		Raylib.ClearBackground(Color.Black);
		Raylib.DrawTexturePro(
			cameraOutput.Texture,
			source,
			new Rectangle(x, 0, newSize),
			Vector2.Zero, 0f, Color.White
		);
	}
}