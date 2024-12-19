using System.Numerics;
using Raylib_cs;

class Clock : Prop
{
	public Model HourHand;
	public Model MinuteHand;
	public Model SecondHand;

	private float hoursHandAngle;
	private float minutesHandAngle;
	private float secondsHandAngle;

	private float yRotation;
	private Vector3 position;

	public Clock(Vector3 position, float yRotation = 0f) : base("./assets/clock-face.glb", position, yRotation)
	{
		// Load the hour, minute, and seconds hand
		HourHand = AssetManager.LoadGlbModel("./assets/clock-hour.glb");
		MinuteHand = AssetManager.LoadGlbModel("./assets/clock-minute.glb");
		SecondHand = AssetManager.LoadGlbModel("./assets/clock-second.glb");

		// TODO: Don't do this
		// Position = position;
		this.yRotation = yRotation;
		this.position = position;
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
		hoursHandAngle = ((float)time.TotalHours) * time12;
		minutesHandAngle = ((float)time.TotalMinutes) * time60;
		secondsHandAngle = ((float)time.TotalSeconds) * time60;

		// Update all the angles
		SetModelTransform(ref HourHand, position, new Vector3(0, yRotation, hoursHandAngle));
		SetModelTransform(ref MinuteHand, position, new Vector3(0, yRotation, minutesHandAngle));
		SetModelTransform(ref SecondHand, position, new Vector3(0, yRotation, secondsHandAngle));
	}

	public override void Render3D()
	{
		// Draw the clock
		base.Render3D();

		// Draw the hands
		Raylib.DrawModel(HourHand, Vector3.Zero, 1f, Color.White);
		Raylib.DrawModel(MinuteHand, Vector3.Zero, 1f, Color.White);
		Raylib.DrawModel(SecondHand, Vector3.Zero, 1f, Color.White);
	}

	public override void Unload()
	{
		// Unload all the hands
		Raylib.UnloadModel(HourHand);
		Raylib.UnloadModel(MinuteHand);
		Raylib.UnloadModel(SecondHand);

		// Unload the rest of the stuff
		base.Unload();
	}
}