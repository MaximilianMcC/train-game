using System.Numerics;
using Raylib_cs;

class Game
{
	public static bool Debug = false;
	public static Track AnchorTrack;

	public static void Run()
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(800, 600, "illegaler");
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
		AnchorTrack = new Track(SetTrack.Straight, new Vector2(100));
		Track straight1 = new Track(SetTrack.Straight, AnchorTrack);
		Track straight2 = new Track(SetTrack.Straight, straight1);
		new Track(SetTrack.Straight, straight2);
	}

	private static void Update()
	{
		// Check for if they want to toggle debug mode
		if (Raylib.IsKeyPressed(KeyboardKey.Grave)) Debug = !Debug;
	}

	private static void Render()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Magenta);

		TrackHandler.Tracks.ForEach(track => track.Render());

		Raylib.EndDrawing();
	}

	private static void CleanUp()
	{
		// Unload everything and whatnot
		Raylib.CloseWindow();
	}
}