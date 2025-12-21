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
			Track previousTrack = Track.Last();
			newTrack.Position = previousTrack.Position + (Vector2.UnitX * previousTrack.Length);
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
	public Vector2 Position;
	private Color debugColor;

	public Track(float length)
	{
		debugColor = Utils.RandomColor();
		Length = length;
		TrackHandler.Add(this);
	}

	public void Draw()
	{
		Raylib.DrawLineEx(Position, Position + (Vector2.UnitX * Length), 15f, debugColor);
	}
}