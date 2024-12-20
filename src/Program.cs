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
				Raylib.ClearBackground(Color.Brown);
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
		Map.Load();
	}

	private static void Update()
	{
		Map.Update();
		Player.Update();
	}

	private static void Render3D()
	{
		Map.Render();
	}

	private static void Render2D()
	{
		Player.Render2D();
		Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 10, 50, Color.White);
	}

	private static void CleanUp()
	{
		Map.Unload();
		Raylib.CloseWindow();
	}
}