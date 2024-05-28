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

    }

    private static void Update()
    {

    }

    private static void Render()
    {
        Raylib.DrawText("im farm", 10, 10, 30, Color.White);
    }

    private static void CleanUp()
    {
        Raylib.CloseWindow();
    }
}