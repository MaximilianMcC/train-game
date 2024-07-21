using System.Numerics;
using Raylib_cs;

class Game
{

	public static void Run()
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(800, 600, "the bro inglenook");
		Raylib.SetTargetFPS(144);

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
		Inglenook.Start();
	}

	private static void Update()
	{
		Inglenook.Update();
	}

	private static void Render()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(new Color(217, 224, 240, 255));

		Inglenook.Render();

		Raylib.EndDrawing();
	}

	private static void CleanUp()
	{
		// Unload everything and whatnot
		Raylib.CloseWindow();
	}
}