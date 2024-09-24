using System.Numerics;
using Raylib_cs;

class SetTrack
{
	// This is all of the shapes of the track
	public Vector2 StartPosition;
	public Vector2 EndPosition;
	public float EndRotation;

	public virtual void Render(Vector2 position, float rotation, Color debugColor)
	{
		// Set the color depending on if we're
		// viewing in debug mode or not
		Color color = Game.Debug ? Color.White : debugColor;

		// Rotate the drawing vectors to match the
		// rotation of the track
		Vector2 rotatedStart = Raymath.Vector2Rotate(StartPosition, rotation * Raylib.DEG2RAD);
		Vector2 rotatedEnd = Raymath.Vector2Rotate(EndPosition, rotation * Raylib.DEG2RAD);

		// Chuck the rotated parts in the correct position
		rotatedStart += position;
		rotatedEnd += position;

		// Draw a line between them idk
		// TODO: Use texture
		Raylib.DrawLineEx(rotatedStart, rotatedEnd, 5f, color);
	}

	// Make it so that we don't need to call the
	// constructor every time we want a new bit of track
	public static SetTrack Straight = new Straight();
	public static SetTrack Curve = new Curve();
}

class Straight : SetTrack
{
	public Straight()
	{
		StartPosition = new Vector2(0, 0);
		EndPosition = new Vector2(100, 0);
		EndRotation = 0f;
	}
}

class Curve : SetTrack
{
	public Curve()
	{
		StartPosition = new Vector2(0, 0);
		EndPosition = new Vector2(50, 50);
		EndRotation = 90f;
	}
}