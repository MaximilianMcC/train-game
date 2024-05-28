using System.Numerics;
using Raylib_cs;

class Player
{
    public static Camera2D Camera;

    private static Texture2D texture;

    public static Rectangle Body;
    private static float speed = 350f;

    public static void Start()
    {
        // Load in the player texture
        texture = Raylib.LoadTexture("./assets/texture/player.png");

        // Define the players body
        Body = new Rectangle(0f, 0f, 64f, 80f);
    }

    public static void Update()
    {
        Movement();
    }

    public static void Render()
    {
        // Draw the player
        Raylib.DrawTexturePro(texture, new Rectangle(0f, 0f, texture.Width, texture.Height), Body, Vector2.Zero, 0f, Color.White);
    }

    public static void CleanUp()
    {
        // Unload the player texture
        Raylib.UnloadTexture(texture);
    }


    // Move the player
    // TODO: Make physics based (velocity)
    private static void Movement()
    {
        // Get the player movement
        // TODO: Make settings file thing (don't hardcode)
        Vector2 movement = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) movement.Y--;
        if (Raylib.IsKeyDown(KeyboardKey.S)) movement.Y++;
        if (Raylib.IsKeyDown(KeyboardKey.A)) movement.X--;
        if (Raylib.IsKeyDown(KeyboardKey.D)) movement.X++;

        // Check for if the player has actually
        // moved, and if so then normalize their
        // movement and apply speed and delta time
        if (movement == Vector2.Zero) return;
        movement = (Vector2.Normalize(movement) * speed) * Raylib.GetFrameTime();

        // Apply the movement to the players body
        Body.Position += movement;
    }
}