using System.Numerics;
using Raylib_cs;

class Train
{
	public Vector2 Position;

	private Railway railway;
	public float PositionOnCurrentTrack;
	public int CurrentTrackIndex;

	private float speed = 200f;

	public Train(Railway railway)
	{
		// Assign the track
		this.railway = railway;
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
		if (Raylib.IsKeyDown(KeyboardKey.Left)) PositionOnCurrentTrack -= speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Right)) PositionOnCurrentTrack += speed * Raylib.GetFrameTime();
	}

	// Actually moving the train
	private void Move()
	{
		// Check for if the train is due to be on the
		// next bit of track and switch them to be there
		CubicBezier track = railway.Track[CurrentTrackIndex];
		if (PositionOnCurrentTrack > track.ArcLength)
		{
			// The current position is taken away so that we don't
			// loose any progress at higher speeds
			if (CurrentTrackIndex < railway.Track.Count - 1)
			{
				PositionOnCurrentTrack = track.ArcLength - PositionOnCurrentTrack;
				CurrentTrackIndex++;
			}
		}
		else if (PositionOnCurrentTrack < 0)
		{
			// The current position is added so that we don't
			// loose any progress at higher speeds
			if (CurrentTrackIndex > 0) 
			{
				PositionOnCurrentTrack = track.ArcLength + PositionOnCurrentTrack;
				CurrentTrackIndex--;
			}
		}

		// Update the position based on the track
		Position = railway.Track[CurrentTrackIndex].GetPositionFromDistance(PositionOnCurrentTrack);
	}

	public void Draw()
	{
		// Circle for now
		Raylib.DrawCircleV(Position, 10f, Color.Magenta);
	}
}