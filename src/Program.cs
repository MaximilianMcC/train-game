using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		// Raylib stuff
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
		Raylib.InitWindow(800, 600, "I have got until February to do this");
		Raylib.SetTargetFPS(144);

		TrackHandler.AnchorPosition = new Vector2(100f);
		Track track0 = new Track(200f);
		Track track1 = new Track(200f);
		Track track2 = new Track(200f);

		while (Raylib.WindowShouldClose() == false)
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			TrackHandler.Draw();
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}