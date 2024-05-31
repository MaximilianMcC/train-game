using System.Numerics;
using Raylib_cs;

class Player
{
	public static Camera2D Camera;

	private static Texture2D texture;

	public static Rectangle Body;
	private static float speed = 85;

	public static void Start()
	{
		// Load in the player texture
		texture = Raylib.LoadTexture("./assets/texture/player.png");

		// Define the players body
		Body = new Rectangle(0f, 0f, 16f, 24f);

		// Setup the camera
		Camera = new Camera2D()
		{
			Offset = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2,
			Rotation = 0f,
			Zoom = 4f
		};
	}

	public static void Update()
	{
		// Move the player
		Movement();

		// Update the cameras position so that it follows
		// the player wherever they go
		// TODO: Maybe only make it track if the player starts to go out of bounds if that makes any sense
		Camera.Target = Body.Position + (Body.Size / 2f);
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

		// Store the current body state so we can
		// add the new positions and whatnot on it. This
		// is done because we check x and y separately
		// so the player can still "slide" if they aren't
		// colliding on a certain axis. Otherwise they would
		// get fully stuck and have to reverse out of it
		Rectangle newBody = Body;

		// Check for x collision
		// TODO: Don't make a new vector for this
		newBody.Position += new Vector2(movement.X, 0);
		if (World.Colliding(newBody)) return;

		// Check for y collision
		newBody.Position += new Vector2(0, movement.Y);
		if (World.Colliding(newBody)) return;

		// Assign the new body to move the player
		// (there was no collision (all goods (moving rn)))
		Body = newBody;	
	}
}