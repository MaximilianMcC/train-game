using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
		Raylib.InitWindow(800, 600, "John track, the inventor of rails (from sheffield btw)");
		Raylib.SetTargetFPS(144);

		Start();
		while (Raylib.WindowShouldClose() == false)
		{
			Update();

			Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.Magenta);
				Raylib.BeginMode3D(Player.Camera);
					Render3D();
				Raylib.EndMode3D();
				Render2D();
			Raylib.EndDrawing();
		}
		CleanUp();
	}

	private static void Start()
	{
		Player.Start();
	}

	private static void Update()
	{
		Player.Update();
	}

	private static void Render3D()
	{
		Raylib.DrawGrid(10, 1);
	}

	private static void Render2D()
	{
		Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 10, 30, Color.White);
	}

	private static void CleanUp()
	{
		// Unload everything and whatnot
		Raylib.CloseWindow();
	}
}