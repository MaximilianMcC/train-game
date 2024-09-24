using System.Numerics;
using Raylib_cs;

class TrackHandler
{
	// Store all of the tracks for
	// rendering and whatnot
	public static List<Track> Tracks = new List<Track>();
}

class Track
{
	public Vector2 Position;
	public SetTrack Type;
	public Track ConnectingTrack;
	private Color debugColor;

	// TODO: Make it so you can supply a number to repeat and it will repeat this track bit x times. Will be useful for straights or something
	public Track(SetTrack type, Track connectingTrack)
	{
		// Assign the type of track and its child
		Type = type;
		ConnectingTrack = connectingTrack;

		// Calculate our position based off
		// our parents position
		Position = connectingTrack.Position + connectingTrack.Type.EndPosition;

		// Make a debug color thing
		Random random = new Random();
		debugColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255), 255);

		// Add ourself to the track list so that
		// we can be rendered and whatnot
		TrackHandler.Tracks.Add(this);
	}

	// Anchor thingy
	// TODO: Don't rewrite all this
	public Track(SetTrack type, Vector2 startingPosition)
	{
		// Assign the type of track and its child
		Type = type;
		ConnectingTrack = null;

		// Calculate our position based off
		// our parents position
		Position = startingPosition;

		// Make a debug color thing
		Random random = new Random();
		debugColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255), 255);

		// Add ourself to the track list so that
		// we can be rendered and whatnot
		TrackHandler.Tracks.Add(this);
	}

	public void Render()
	{
		// Render the type of track at
		// the position of the track rn
		Type.Render(Position, debugColor);
	}
}