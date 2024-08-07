using System.Numerics;
using Raylib_cs;

class Locomotive
{
	public Vector2 Position;
	public float trackPosition;

	private Railway railway;
	private int trackIndex;
	private CubicBezier track;

	private float speed = 200f;


	public Locomotive(Railway railway, int trackIndex = 0)
	{
		// Assign the track
		this.railway = railway;

		// Set the initial track to spawn on
		this.trackIndex = trackIndex;
		track = railway.Track[trackIndex];

		// Make the spawn position in the middle
		// of the first part of track
		trackPosition = track.Length / 2;
	}

	public void Update()
	{
		Footplate();
		Move();
	}

	// Controlling the train
	private void Footplate()
	{
		// Check for if we wanna move the train
		// TODO: Rewrite to act like a train and not player
		if (Raylib.IsKeyDown(KeyboardKey.Left)) trackPosition -= speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Right)) trackPosition += speed * Raylib.GetFrameTime();
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
			//! Might not work for high speeds
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
			//! Might not work for high speeds
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
		// Circle for now
		Raylib.DrawCircleV(Position, 10f, Color.Magenta);
	}
}