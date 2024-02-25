using System.Numerics;
using Raylib_cs;

class Player
{
	private static float mass = 45f;
	private static float moveForce;
	private static float jumpForce;

	private static float width = 32f;
	private static float height = 64f;

	public static Vector2 Position { get; set; }
	public static Vector2 Velocity { get; set; }

	public static void Start()
	{
		// Set the forces dependant on the mass
		moveForce = mass * 15.5f;
		jumpForce = mass * 15f;
	}

	public static void Update()
	{
		Movement();
	}

	public static void Render()
	{
		Raylib.DrawText($"Velocity: {Velocity}", 0, 0, 30, Color.Black);

		Raylib.DrawRectangleRec(new Rectangle(Position.X, Position.Y, width, height), Color.Red);
	}

	public static void CleanUp()
	{
		
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
}