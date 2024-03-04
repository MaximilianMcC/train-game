using System.Numerics;
using Raylib_cs;

class Game
{
	public const int GameWidth = 512;
	public const int GameHeight = 256;
	private static RenderTexture2D gameView;

	public static void Run()
	{
		//!
		Raylib.SetTraceLogLevel(TraceLogLevel.Error);

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
		// Make a render texture to
		// render the game on so that
		// it stays the same sorta size
		// no-matter the window/screen
		gameView = Raylib.LoadRenderTexture(GameWidth, GameHeight);

		// Load in the map
		Map.LoadMap("./maps/map.map");

		Player.Start();
	}
	
	private static void Update()
	{
		Player.Update();
	}

	private static void Render()
	{
		Raylib.BeginDrawing();

		// Draw the game to the game view
		Raylib.BeginTextureMode(gameView);
		Raylib.BeginMode2D(Player.Camera);
		Raylib.ClearBackground(Color.Magenta);


		Map.Render();
		Player.Render();

		Raylib.EndMode2D();
		Raylib.EndTextureMode();

		// Draw the game on the screen
		Rectangle source = new Rectangle(0, 0, GameWidth, -GameHeight);
		Rectangle output = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
		Raylib.DrawTexturePro(gameView.Texture, source, output, Vector2.Zero, 0f, Color.White);


		// Draw UI stuff
		Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 10, 30, Color.Black);
		Raylib.DrawText($"Position: {Player.Position}", 10, 40, 30, Color.Black);


		Raylib.EndDrawing();
	}

	private static void CleanUp()
	{
		Map.CleanUp();
		Player.CleanUp();

		//! do this last
		Raylib.CloseWindow();
	}
}