using Raylib_cs;

class Game
{
	public static void Run()
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(800, 600, "train game (traim)");

		Start();
		while (Raylib.WindowShouldClose() == false)
		{
			Update();
			Render();
		}
		CleanUp();
	}

	private static void Start()
	{

	}

	private static void Update()
	{

	}

	private static void Render()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Magenta);

		Raylib.DrawText("train", 10, 10, 30, Color.White);

		Raylib.EndDrawing();
	}

	private static void CleanUp()
	{
		// Unload everything and whatnot
		Raylib.CloseWindow();
	}
}