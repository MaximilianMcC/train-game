using System.Numerics;
using Raylib_cs;

class Program
{
	private static Spline spline;
	private static float testPosition = 0;

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
        spline = new Spline(
			new Vector2(0, 0) * 100,
			new Vector2(1, 2) * 100,
			new Vector2(3, 3) * 100,
			new Vector2(4, 0) * 100
		);
	}

	private static void Update()
	{
		testPosition += 0.2f * Raylib.GetFrameTime();
		if (testPosition > 1) testPosition = 0;
	}

	private static void Render()
	{
		// spline.DrawSpline(Vector2.Zero, 5f, Color.White);
		spline.Draw(new Vector2(100, 100), 5f, Color.Red, 200);
		Raylib.DrawCircleV(new Vector2(100, 100) + spline.GetPosition(testPosition), 6f, Color.Pink);
	}

	private static void CleanUp()
	{
		Raylib.CloseWindow();
	}
}