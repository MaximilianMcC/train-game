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

		TrackHandler.AnchorPosition = new Vector2(100f, 500f);

		Vector2 horizontal = Vector2.UnitX;

		Track headShunt = new Track(200f, horizontal);
		TrackPoint points = new TrackPoint(100f, horizontal);

		Track top = new Track(100f, horizontal);

		Track bottom = new Track(100f, horizontal);
		TrackHandler.LinkBranch(points, bottom);




		Locomotive locomotive = new Locomotive(headShunt, 0f);

		while (Raylib.WindowShouldClose() == false)
		{
			locomotive.Update();

			if (Raylib.IsKeyPressed(KeyboardKey.Space)) points.Switch();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			TrackHandler.DrawAllTrack();
			locomotive.Draw();
			Raylib.EndDrawing();
		}

		locomotive.Unload();

		Raylib.CloseWindow();
	}
}