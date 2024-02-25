using Raylib_cs;

class Game
{
	public static void Run()
	{
		// Make the raylib window and whatnot
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(650, 500, "Platformer");
		Raylib.SetTargetFPS(144);

		Start();
		while (!Raylib.WindowShouldClose())
		{
			Update();
			Render();
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

	private static void Render()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Magenta);

		Player.Render();
		Raylib.DrawText("platofrming rn", 100, 100, 45, Color.Beige);

		Raylib.EndDrawing();
	}

	private static void CleanUp()
	{
		Player.CleanUp();

		//! do this last
		Raylib.CloseWindow();
	}
}