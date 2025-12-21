using System.Numerics;
using Raylib_cs;

class TrackHandler
{
	public static List<Track> Track = [];
	public static Vector2 AnchorPosition;
	
	public static void Add(Track newTrack)
	{
		if (Track.Count != 0) LinkTracks(Track.Last(), newTrack);
		else newTrack.Position = AnchorPosition;

		Track.Add(newTrack);
	}

	public static void LinkTracks(Track left, Track right)
	{
		// 'mechanically' link them
		left.Next = right;
		right.Previous = left;

		// visually link them
		right.Position = left.EndPosition;
	}

	public static void LinkBranch(TrackPoint point, Track track)
	{
		// Erase the old bit from the auto add thing yk
		track.Previous.Next = null;

		// 'mechanically' link them
		point.Branch = track;
		track.Previous = point;

		// visually link them
		track.Position = point.BranchEndPosition;
	}

	public static void DrawAllTrack()
	{
		foreach (Track track in Track)
		{
			track.Draw();
		}
	}
}

class Track
{
	public virtual Track Next { get; set; } = null;
	public Track Previous { get; set; } = null;

	public float Length;
	public Vector2 Direction;
	protected Color debugColor;

	public Vector2 Position;
	public Vector2 EndPosition => Position + (Direction * Length);

	public Track(float length, Vector2 direction)
	{
		debugColor = Utils.RandomColor();

		Length = length;
		Direction = direction;

		TrackHandler.Add(this);
	}

	public virtual void Draw()
	{
		Raylib.DrawLineEx(Position, EndPosition, 15f, debugColor);
	}
}

class TrackPoint : Track
{
	public bool Switched = false;
	public Track Branch = null;

	// private Vector2 branchDirection => Vector2.Normalize(Direction + new Vector2(0.5f));
	private Vector2 branchDirection => Direction + new Vector2(0.75f);
	public Vector2 BranchEndPosition => Position + ((Direction + branchDirection) * Length * 0.5f);

	public override Track Next => Switched ? Branch : base.Next;

	public TrackPoint(float length, Vector2 direction)
	: base(length, direction) { }

	public void Switch() => Switched = !Switched;

	public override void Draw()
	{
		// Draw the two tracks
		base.Draw();
		Raylib.DrawLineEx(Position, BranchEndPosition, 15f, debugColor);

		// Draw the switch indicator
		string indicator = Switched ? "l" : "-";
		Raylib.DrawText(indicator, (int)Position.X, (int)Position.Y, 50, Color.White);
	}
}