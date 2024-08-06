using System.Numerics;
using Raylib_cs;

class Train
{
	public Vector2 Position;

	private List<CubicBezier> railway;
	public float PositionOnCurrentTrack;
	public int CurrentTrackIndex;

	private float speed = 200f;

	public Train(List<CubicBezier> railway)
	{
		// Assign the track
		this.railway = railway;

		// Set the start position to be at the
		// start of the first bit of track
		PositionOnCurrentTrack = 0f;
		Position = this.railway[0].GetPositionFromDistance(PositionOnCurrentTrack);
	}

	public void Update()
	{
		// Check for if we wanna move the train
		// TODO: Rewrite to act like a train and not player
		if (Raylib.IsKeyDown(KeyboardKey.Left)) PositionOnCurrentTrack -= speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Right)) PositionOnCurrentTrack += speed * Raylib.GetFrameTime();

		// Check for if the train is due to be on the
		// next bit of track and switch them to be there
		CubicBezier track = railway[CurrentTrackIndex];
		if (PositionOnCurrentTrack > track.ArcLength)
		{
			// The current position is taken away so that we don't
			// loose any progress at higher speeds
			if (CurrentTrackIndex < railway.Count - 1)
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
		Position = railway[CurrentTrackIndex].GetPositionFromDistance(PositionOnCurrentTrack);
	}

	public void Render()
	{
		// Circle for now
		Raylib.DrawCircleV(Position, 10f, Color.Magenta);
	}
}