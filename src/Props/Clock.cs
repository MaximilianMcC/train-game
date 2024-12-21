using System.Numerics;
using Raylib_cs;

class Clock : Prop
{
	// TODO: Don't do this
	private Vector3 position;
	private float yRotation;

	public Clock(Vector3 position, float yRotation = 0f)
	{
		// Load the clock, as well as all the hands
		Models.Add("face", AssetManager.LoadGlbModel("./assets/clock-face.glb"));
		Models.Add("hourHand", AssetManager.LoadGlbModel("./assets/clock-hour.glb"));
		Models.Add("minuteHand", AssetManager.LoadGlbModel("./assets/clock-minute.glb"));
		Models.Add("secondHand", AssetManager.LoadGlbModel("./assets/clock-second.glb"));

		// Set the initial positions and rotations for everything
		foreach (string modelName in Models.Keys)
		{
			SetMatrix(modelName, GetMatrix(position, Vector3.UnitY * yRotation, 1f));
		}

		// TODO: Don't do this
		this.position = position;
		this.yRotation = yRotation;
	}

	public override void Update()
	{
		// Get the current irl time
		// TODO: Use some sorta in-game time
		TimeSpan time = DateTime.Now.TimeOfDay;

		// Get all the time scalers or whatever these are
		float time60 = 360 / 60;
		float time12 = 360 / 12;

		// Calculate all the angles
		float hoursHandAngle = ((float)time.TotalHours) * time12;
		float minutesHandAngle = ((float)time.TotalMinutes) * time60;
		float secondsHandAngle = ((float)time.TotalSeconds) * time60;

		// Update all the angles
		SetMatrix("hourHand", GetMatrix(position, new Vector3(0, yRotation, hoursHandAngle)));
		SetMatrix("minuteHand", GetMatrix(position, new Vector3(0, yRotation, minutesHandAngle)));
		SetMatrix("secondHand", GetMatrix(position, new Vector3(0, yRotation, secondsHandAngle)));
	}
}