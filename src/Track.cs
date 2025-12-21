using System.Numerics;
using Raylib_cs;

class TrackHandler
{
	public static List<Track> Track = [];
	public static Vector2 AnchorPosition;
	
	public static void Add(Track newTrack)
	{
		if (Track.Count != 0)
		{
			// Sort out its position
			Track previousTrack = Track.Last();
			newTrack.Position = previousTrack.EndPosition;

			// 'link' the two tracks together
			newTrack.Previous = previousTrack;
			previousTrack.Next = newTrack;
		}
		else newTrack.Position = AnchorPosition;

		Track.Add(newTrack);
	}

	public static void Draw()
	{
		foreach (Track track in Track)
		{
			track.Draw();
		}
	}
}

class Track
{
	public Track Next = null;
	public Track Previous = null;

	public float Length;
	public Vector2 Direction;
	private Color debugColor;

	public Vector2 Position;
	public Vector2 EndPosition => Position + (Direction * Length);

	public Track(float length, Vector2 direction)
	{
		debugColor = Utils.RandomColor();

		Length = length;
		Direction = direction;

		TrackHandler.Add(this);
	}

	public void Draw()
	{
		Raylib.DrawLineEx(Position, EndPosition, 15f, debugColor);
	}
}