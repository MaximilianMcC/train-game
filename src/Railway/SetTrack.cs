using System.Numerics;
using Raylib_cs;

class SetTrack
{
	// This is all of the shapes of the track
	public Vector2 StartPosition;
	public Vector2 EndPosition;

	public virtual void Render(Vector2 position, Color debugColor)
	{
		// Set the color depending on if we're
		// viewing in debug mode or not
		Color color = Game.Debug ? Color.White : debugColor;

		// Draw a line between them idk
		// TODO: Use texture
		Raylib.DrawLineEx(StartPosition + position, EndPosition + position, 5f, color);
	}

	// Make it so that we don't need to call the
	// constructor every time we want a new bit of track
	public static SetTrack Straight = new Straight();
}

class Straight : SetTrack
{
	public Straight()
	{
		StartPosition = new Vector2(0, 0);
		// EndPosition = new Vector2(1, 0);
		EndPosition = new Vector2(100, 0);
	}

	// todo: add other render thingys here
	// public override void Render()
}