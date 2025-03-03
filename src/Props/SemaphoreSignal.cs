using System.Numerics;
using Raylib_cs;

class SemaphoreSignal : Prop
{
	public bool Danger = true;
	private float clearAngle = -45f;

	private float armRotation = 0;
	private Vector3 armOffset = new Vector3(0, 5.21f, 0);

	// TODO: Don't put here
	private const float Gravity = 9.81f;

	// private float armWeight = 25f * Gravity;
	private float armWeight = Gravity;
	private float armVelocity = 0;
	private float bounceFactor = 0.7f;
	private bool simulating = false;

	public SemaphoreSignal(Vector3 position, float yRotation = 0f)
	{
		Position = position;

		// Load the models
		Models.Add("post", AssetManager.LoadGlbModel("./assets/semaphore-post.glb"));
		Models.Add("arm", AssetManager.LoadGlbModel("./assets/semaphore-arm2.glb"));

		// Set the initial positions and rotations for them
		SetMatrix("post", GenerateMatrix(Position, Vector3.UnitY * yRotation, 1f));
		SetMatrix("arm", GenerateMatrix(Position + armOffset, new Vector3(0, 0, armRotation)));
	}

	public override void Update()
	{
		// debug toggle
		if (Raylib.IsKeyPressed(KeyboardKey.Space))
		{
			// Toggle the state
			Danger = !Danger;

			// Begin the simulation
			simulating = true;
		}

		if (simulating) SimulateArm();
	}

	private void SimulateArm()
	{
		// Apply the force to move the arm
		//? gravity working both for up and down here
		//TODO: Make going up slower and stutter in middle (person doing it)
		int direction = Danger ? 1 : -1;
		armVelocity += (armWeight * direction) * Raylib.GetFrameTime();

		// Rotate the arm
		armRotation += armVelocity * Raylib.GetFrameTime();

		// Check if we need to bounce the arm
		float finalPosition = (Danger ? 0 : clearAngle);
		if (armRotation >= finalPosition)
		{
			// Ensure we are actually on the ground
			armRotation = finalPosition;

			// Flip the direction of the bounce
			// and make the next bounce not as
			// strong as the current (losing energy)
			armVelocity = -armVelocity * bounceFactor;

			// If the velocity is small then
			// the simulation is probably over
			if ((int)armVelocity == 0)
			{
				armVelocity = 0f;
				simulating = false;
			}
		}

		// Update the model or whatever
		SetMatrix("arm", GenerateMatrix(Position + armOffset, new Vector3(0, 0, armRotation)));
	}

    public override void RenderDebug2D()
    {
        Raylib.DrawText($"{simulating}\n{armRotation}\n{(Danger ? "danger" : "clear")}", 10, 50, 30, Color.White);
    }

}