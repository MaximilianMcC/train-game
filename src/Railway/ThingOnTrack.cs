using System.Numerics;
using Raylib_cs;

class ThingOnTrack
{
	public Vector2 Position;
	public float trackPosition;

	public Railway Railway;
	private int trackIndex;
	private CubicBezier track;

	public ThingOnTrack(Railway railway, int trackIndex = 0)
	{
		// Assign the track
		Railway = railway;
		this.trackIndex = trackIndex;
		track = railway.Track[trackIndex];

		// Make the spawn position in the middle
		// of the first part of track
		trackPosition = track.Length / 2;
	}

	public virtual void Update()
	{
		Move();
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
			if (Railway.Track.Count <= trackIndex + 1)
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
			trackPosition = Railway.Track[trackIndex].Length;
		}

		// Get the new bit of track and
		// update the position based on it
		track = Railway.Track[trackIndex];
		Position = track.GetPositionFromDistance(trackPosition);
	}

	public virtual void Draw() { }

	protected virtual void CleanUp() { }
}