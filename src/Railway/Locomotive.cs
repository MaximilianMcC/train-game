using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Locomotive
{
	public Vector2 Position;
	public float trackPosition;

	private Railway railway;
	private int trackIndex;
	private CubicBezier track;

	private RenderTexture2D footplate;
	private float regulator = 0;
	private bool reversing;

	public Locomotive(Railway railway, int trackIndex = 0)
	{
		// Assign the track
		this.railway = railway;
		this.trackIndex = trackIndex;
		track = railway.Track[trackIndex];

		// Make the spawn position in the middle
		// of the first part of track
		trackPosition = track.Length / 2;

		// Make the render texture to draw the footplate on
		footplate = Raylib.LoadRenderTexture(250, 250);
	}

	public void Update()
	{
		Footplate();
		Move();
	}

	// Controlling the train
	private void Footplate()
	{
		// Check for if we want to open/close the regulator
		float regulatorSpeed = 0.5f * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) regulator -= regulatorSpeed;
		if (Raylib.IsKeyDown(KeyboardKey.Right)) regulator += regulatorSpeed;

		// Clamp the regulator to a value between 0-1
		regulator = Math.Clamp(regulator, 0, 1);

		// Figure out what the current speed should be based
		// on how much steam the regulator is letting through
		float speed = (250 * regulator) * Raylib.GetFrameTime();
		
		// Check for if we're in reversing gear or not. To switch
		// gears the train must be stopped
		//? idk if this is actually accurate because I swear you can change it while moving
		if (speed == 0 && Raylib.IsKeyPressed(KeyboardKey.R)) reversing = !reversing;

		// Move the locomotive
		// TODO: Apply a force (friction based movement)
		trackPosition += reversing ? -speed : speed;
	}

	// Actually moving the train
	private void Move()
	{
		// Check for if we're about to move
		// onto the next section of track
		if (trackPosition > track.Length)
		{
			// Check for if there is another valid section
			// of track that we can move to
			if (railway.Track.Count <= trackIndex + 1)
			{
				// No more track. Do nothing
				trackPosition = track.Length;
				return;
			}
		
			// Move onto the next section of track
			trackIndex++;

			// Reset the position since we've 
			// moved onto a new piece of track
			//! Might not work for higher speeds
			trackPosition = 0f;
		}
		else if (trackPosition < 0f)
		{
			// Check for if there is another valid section
			// of track that we can move to
			if (trackIndex <= 0f)
			{
				// No more track. Do nothing
				trackPosition = 0f;
				return;
			}
		
			// Move back to the previous section of track
			trackIndex--;

			// Reset the position since we've 
			// moved onto a new piece of track
			//! Might not work for higher speeds
			// TODO: Don't get length like this
			trackPosition = railway.Track[trackIndex].Length;
		}

		// Get the new bit of track and
		// update the position based on it
		track = railway.Track[trackIndex];
		Position = track.GetPositionFromDistance(trackPosition);
	}

	public void Draw()
	{
		// Draw the actual locomotive (circle for now)
		Raylib.DrawCircleV(Position, 10f, Color.Magenta);

		// Draw the footplate
		RenderFootplate();
		Raylib.DrawTextureEx(footplate.Texture, new Vector2(10 + 250, Raylib.GetScreenHeight() - 260), 180f, 0.8f, Color.White);

		// Say if we're reversing or not and the regulator value
		// TODO: Show visually with levers and whatnot
		Raylib.DrawText($"{regulator.ToString("0.0")}\n\n{reversing}", Raylib.GetScreenWidth() - 150, Raylib.GetScreenHeight() - 70, 40, Color.Black);

		if (Game.Debug)
		{
			Raylib.DrawText($"Regulator: {regulator}", 10, 10, 50, Color.Brown);
		}
	}


	private void RenderFootplate()
	{
		// Begin drawing
		Raylib.BeginTextureMode(footplate);
		Raylib.ClearBackground(Color.Black);

		// Draw the regulator
		float angle = Utils.ConvertRange(regulator, 120, 60);
		Utils.DrawAngledLine(new Vector2(125, 200), 100f, 120, 5f, new Color(16, 16, 16, 255));
		Utils.DrawAngledLine(new Vector2(125, 200), 100f, 60, 5f, new Color(16, 16, 16, 255));
		Utils.DrawAngledLine(new Vector2(125, 200), 150f, angle, 10f, Color.Gray);

		// End drawing
		Raylib.EndTextureMode();
	}

	private void CleanUp()
	{
		// Get rid of the footplate
		Raylib.UnloadRenderTexture(footplate);
	}
}