using System.Numerics;
using Raylib_cs;

class Program
{
	private static RenderTexture2D cameraOutput;

	public static void Main(string[] args)
	{
		// Raylib stuff
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
		Raylib.InitWindow(800, 600, "John track, the inventor of rails (from sheffield btw)");
		Raylib.SetTargetFPS(144);

		// Make the camera output render texture
		// so that we can render the game pixelated
		cameraOutput = Raylib.LoadRenderTexture(854, 480);

		// Game stuff
		Start();
		while (Raylib.WindowShouldClose() == false)
		{
			Update();

			Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.Magenta);

				// Render 3D stuff to the output texture
				Raylib.BeginTextureMode(cameraOutput);
					Raylib.ClearBackground(Color.Brown);
					Raylib.BeginMode3D(Player.Camera);
						Render3D();
					Raylib.EndMode3D();
				Raylib.EndTextureMode();
				
				// Draw the render texture blown up
				Raylib.DrawTexturePro(
					cameraOutput.Texture,
					new Rectangle(0, 0, cameraOutput.Texture.Width, -cameraOutput.Texture.Height),
					new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()),
					Vector2.Zero,
					0f,
					Color.White
				);

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
		Raylib.UnloadRenderTexture(cameraOutput);
		Map.Unload();
		Raylib.CloseWindow();
	}
}