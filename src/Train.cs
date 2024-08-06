using System.Numerics;
using Raylib_cs;

class Train
{
	public Vector2 Position;

	private List<CubicBezier> track;
	public float PositionOnCurrentTrack;
	public int CurrentTrackIndex;


	public Train(List<CubicBezier> railway)
	{
		// Assign the track
		track = railway;

		// Set the start position to be at the
		// start of the first bit of track
		PositionOnCurrentTrack = 0f;
		Position = track[0].GetPositionFromDistance(PositionOnCurrentTrack);
	}

	public void Update()
	{
		// Check for if we wanna move the train
		// TODO: Rewrite to act like a train and not player
		if (Raylib.IsKeyDown(KeyboardKey.Right)) PositionOnCurrentTrack += 100 * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) PositionOnCurrentTrack -= 100 * Raylib.GetFrameTime();

		// Update the position based on the track
		Position = track[CurrentTrackIndex].GetPositionFromDistance(PositionOnCurrentTrack);
	}

	public void Render()
	{
		// Circle for now
		Raylib.DrawCircleV(Position, 10f, Color.Blue);
	}
}