using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		// Raylib stuff
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
		Raylib.InitWindow(800, 600, "r");
		Raylib.SetTargetFPS(144);

		// Game stuff
		Start();
		while (Raylib.WindowShouldClose() == false)
		{
			Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			Render();

			Raylib.EndDrawing();
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
	}

	private static void CleanUp()
	{
		Raylib.CloseWindow();
	}
}