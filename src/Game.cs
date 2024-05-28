using Raylib_cs;

class Game
{
    public static void Run()
    {
        // Raylib stuff
        Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
        Raylib.InitWindow(800, 600, "farmer (farming rn)");
        Raylib.SetTargetFPS(144);

        // Main game loop
        Start();
        while (!Raylib.WindowShouldClose())
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
        Player.Start();
    }

    private static void Update()
    {
        Player.Update();
    }

    private static void Render()
    {
        // Draw world stuff (follows the player in the world)
        // Raylib.BeginMode2D(Player.Camera);
        Player.Render();
        // Raylib.EndMode2D();

        // Draw UI stuff (hasn't got a world position)
        Raylib.DrawText("im farm", 10, 10, 30, Color.White);
    }

    private static void CleanUp()
    {
        Player.CleanUp();

        // Kill all the raylib stuff
        //! Make sure this is done last
        Raylib.CloseWindow();
    }
}