using System.Numerics;
using Raylib_cs;

class Game
{
	private static Sprite test;

	public static void Run()
	{
		// Raylib stuff
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(800, 600, "farmer (farming rn)");
		Raylib.SetTargetFPS(144);
		Raylib.SetExitKey(KeyboardKey.Null);

		// Main game loop
		Start();
		while (!Raylib.WindowShouldClose())
		{
			// Update everything
			Update();

			// Draw everything
			Raylib.BeginDrawing();
			Render();
			Raylib.EndDrawing();
		}
		CleanUp();
	}

	private static void Start()
	{
		test = new Sprite("./assets/sprites/box.png", 16, 16);
		test.Position = new Vector2(200);
	}

	private static void Update()
	{
		test.Rotation += 0.1f;
	}

	private static void Render()
	{
		// Clear the previous thing drawn
		Raylib.ClearBackground(Color.Magenta);
		Raylib.DrawText("im farm", 10, 10, 45, Color.White);

		test.Render();
	}

	private static void CleanUp()
	{
		test.CleanUp();

		// Kill all the raylib stuff
		//! Make sure this is done last
		Raylib.CloseWindow();
	}
}