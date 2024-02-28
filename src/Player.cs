using System.Numerics;
using Raylib_cs;

class Player
{
	// Camera stuff
	public static Camera2D Camera;

	private static float mass = 45f;
	private static float moveForce;
	private static float jumpForce;

	private static float width = 32f;
	private static float height = 64f;

	public static Vector2 Position { get; set; }
	public static Vector2 Velocity { get; set; }

	// Animation and texture stuff
	// TODO: Make fps dependant on velocity
	private static Texture2D[] animationFrames;
	private static Texture2D idleTexture;
	private static int animationFrame = 0;
	private static float animationFps = 10f;
	private static double timeSinceLastAnimationFrame;

	public static void Start()
	{
		// Setup the camera
		Camera = new Camera2D()
		{
			Target = Position,
			Offset = new Vector2(Game.GameWidth, Game.GameHeight) / 2,
			Rotation = 0.0f,
			Zoom = 1f
		};

		// Set the forces dependant on the mass
		// moveForce = mass * 10.5f;
		moveForce = mass * 10.5f;
		jumpForce = mass * 10f;

		// Get animation info
		// TODO: Do somewhere else. Maybe in JSON or just by looking at directory
		const int animationFrameCount = 4;
		animationFrames = new Texture2D[animationFrameCount];
		const string animationPath = "./assets/texture/player/walk-";
		timeSinceLastAnimationFrame = Raylib.GetTime();

		// Load in all of the animation frames
		for (int i = 0; i < animationFrameCount; i++)
		{
			animationFrames[i] = Raylib.LoadTexture(animationPath + i + ".png");
		}

		// Load in the other textures
		idleTexture = Raylib.LoadTexture("./assets/texture/player/idle.png");
	}

	public static void Update()
	{
		Movement();
		Animate();

		// Make the camera follow the player
		Camera.Target = Position;
	}

	public static void Render()
	{
		Raylib.DrawText($"Velocity: {Velocity}", 0, 0, 30, Color.Black);

		// Raylib.DrawTexture(animationFrames[animationFrame], (int)Position.X, (int)Position.Y, Color.White);

		// Check for if the player is moving or not
		if (Velocity.X == 0f)
		{
			// Draw the player normally standing
			// still and facing the camera
			// TODO: Reset the walk cycle to look more smooth/fluent
			Raylib.DrawTextureV(idleTexture, Position, Color.White);
		}
		else
		{
			// Draw the player with the walking animation
			// depending on what direction they are going
			Rectangle source = new Rectangle(0f, 0f, width, height);

			// Flip texture to make it look like walking left
			if (Velocity.X < 0) source.Width = -width;

			// Draw the player
			Raylib.DrawTextureRec(animationFrames[animationFrame], source, Position, Color.White);
		}

	}

	public static void CleanUp()
	{
		// Unload all of the animation frames
		foreach (Texture2D frame in animationFrames) Raylib.UnloadTexture(frame);

		// Unload all of the other textures
		Raylib.UnloadTexture(idleTexture);
	}





	private static void Movement()
	{
		// Get the user input and move the player
		Vector2 direction = Vector2.Zero;
		if (Raylib.IsKeyDown(Settings.Controls.Left) || Raylib.IsKeyDown(Settings.Controls.LeftAlt)) direction.X--;
		if (Raylib.IsKeyDown(Settings.Controls.Right) || Raylib.IsKeyDown(Settings.Controls.RightAlt)) direction.X++;

		// Apply friction to slow down the player overtime
		// TODO: Don't use vectors like this
		const float frictionCoefficient = 0.01f;
		Velocity = new Vector2((Velocity.X * -frictionCoefficient), Velocity.Y);
		if (Math.Abs(Velocity.X) < 0.1f) Velocity = new Vector2(0f, Velocity.Y);

		// Apply the movement
		Velocity += (direction * moveForce) * Raylib.GetFrameTime();
		
		// Actually move the player
		Vector2 newPosition = Position + Velocity;
		if (Collision(newPosition) == false) Position = newPosition;
	}

	private static bool Collision(Vector2 newPosition)
	{
		return false;
	}

	// TODO: only animate if the player is moving
	private static void Animate()
	{
		// Check for if we're eligible for the next frame
		double currentTime = Raylib.GetTime();
		double elapsedTime = currentTime - timeSinceLastAnimationFrame;
		if (elapsedTime >= (1f / animationFps))
		{
			// Go to the next frame
			animationFrame++;
			if (animationFrame > animationFrames.Length - 1) animationFrame = 0;

			// Reset the time
			timeSinceLastAnimationFrame = currentTime;
		}
	}
}