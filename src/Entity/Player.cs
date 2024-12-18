using System.Numerics;
using Raylib_cs;

class Player
{
	// General stuff
	public static Vector3 Position;
	public static Quaternion Rotation;

	// 3D stuff
	public static Camera3D Camera;
	private static float yaw, pitch;
	private static Quaternion headRotation;

	// Player stats
	private static float height = 1.75f;
	private static float eyeHeight = 1.645f;
	private static float movementSpeed = 6f;

	public static void Start()
	{
		// Create the camera
		Camera = new Camera3D()
		{
			Up = Vector3.UnitY,
			FovY = 65f,
			Projection = CameraProjection.Perspective
		};

		// Turn off the mouse
		Raylib.DisableCursor();
	}

	public static void Update()
	{
		UpdateCamera();

		UpdateRotation();
		UpdatePosition();
	}

	private static void UpdateCamera()
	{
		// Get the direction that we're looking
		Vector3 direction = Vector3.Transform(Vector3.UnitZ, headRotation);

		// Get the current camera position
		// and update the camera accordingly
		Vector3 cameraPosition = Position + (Vector3.UnitY * eyeHeight);
		Camera.Position = cameraPosition;
		Camera.Target = cameraPosition + direction;
	}

	// Move the players head
	private static void UpdateRotation()
	{
		// Get the new yaw and pitch (look around)
		Vector2 mouseMovement = Raylib.GetMouseDelta() * InputManager.Sensitivity;
		yaw -= mouseMovement.X;
		pitch = Math.Clamp(pitch + mouseMovement.Y, -89f, 89f);

		// Update the quaternion rotation
		float yawRadians = yaw * Raylib.DEG2RAD;
		float pitchRadians = pitch * Raylib.DEG2RAD;
		headRotation = Quaternion.CreateFromYawPitchRoll(yawRadians, pitchRadians, 0f);
		Rotation = Quaternion.CreateFromYawPitchRoll(yawRadians, 0f, 0f);
	}

	// Move the players body
	private static void UpdatePosition()
	{
		// Get the movement input
		Vector3 inputDirection = InputManager.GetDirectionInput();

		// Create direction vectors based on player rotation
		Vector3 forwards = Vector3.Transform(Vector3.UnitZ, Rotation);
		Vector3 right = Vector3.Cross(Camera.Up, forwards);

		// Combine the directions to get the final movement direction
		Vector3 direction = (forwards * inputDirection.Z) + (right * inputDirection.X);

		// Get the new players position
		float movement = movementSpeed;
		Vector3 potentialNewPosition = Position + (direction * movement) * Raylib.GetFrameTime();

		// TODO: Make the size an actual thing
		// Make a bounding box based on the new position
		Vector3 size = new Vector3(0.5f, height, 0.5f);
		BoundingBox hitbox = new BoundingBox(potentialNewPosition, potentialNewPosition + size);

		// Check for collision. If there wasn't any then
		// set the players position to be this new one
		bool collision = Map.Collision(hitbox);
		if (collision == false) Position = potentialNewPosition;
	}
}