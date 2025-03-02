using System.Numerics;
using Raylib_cs;

class SemaphoreSignal : Prop
{
	public bool Danger = false;
	private Vector3 armOffset = new Vector3(0, 5.21f, 0);

	// TODO: Don't put here
	private const float Gravity = 9.81f;

	private float armMass = 25f * Gravity;
	private float armVelocity = 0;
	private float armRotation = -45;
	private float bounceFactor = 0.7f;

	public SemaphoreSignal(Vector3 position, float yRotation = 0f)
	{
		Position = position;

		// Load the models
		Models.Add("post", AssetManager.LoadGlbModel("./assets/semaphore-post.glb"));
		Models.Add("arm", AssetManager.LoadGlbModel("./assets/semaphore-arm.glb"));

		// Set the initial positions and rotations for them
		SetMatrix("post", GenerateMatrix(Position, Vector3.UnitY * yRotation, 1f));
		SetMatrix("arm", GenerateMatrix(Position, Vector3.UnitY * yRotation, 1f));
	}

	public override void Update()
	{
		// debug toggle
		if (Raylib.IsKeyPressed(KeyboardKey.Space))
		{
			// Toggle the state
			Danger = !Danger;
		}

		// Move the arm
		armVelocity += armMass * Raylib.GetFrameTime();
		armRotation += armVelocity * Raylib.GetFrameTime();

		// Check if we need to bounce the arm
		if (armRotation >= 0)
		{
			// Ensure we are actually on the ground
			armRotation = 0f;

			// Flip the direction of the bounce
			// and make the next bounce not as
			// strong as the current (losing energy)
			armVelocity = -armVelocity * bounceFactor;
		}

		// Update the model or whatever
		SetMatrix("arm", GenerateMatrix(Position + armOffset, new Vector3(0, 0, armRotation)));
	}

}