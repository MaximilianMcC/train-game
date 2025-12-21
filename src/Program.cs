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

		TrackHandler.AnchorPosition = new Vector2(100f);
		Track track0 = new Track(200f, Vector2.UnitX);
		Track track1 = new Track(200f, new Vector2(0.5f));
		Track track2 = new Track(200f, Vector2.UnitX);

		Locomotive locomotive = new Locomotive(track0, 0f);

		while (Raylib.WindowShouldClose() == false)
		{
			locomotive.Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			TrackHandler.Draw();
			locomotive.Draw();
			Raylib.EndDrawing();
		}

		locomotive.Unload();

		Raylib.CloseWindow();
	}
}