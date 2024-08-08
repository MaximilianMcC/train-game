using System.Numerics;
using Raylib_cs;

class Railway
{
	public Vector2 Origin { get; private set; }
	public List<CubicBezier> Track { get; private set; }
	public bool ClosedLoop { get; private set; }
	private Color debugColor;

	public Railway(Vector2 startPosition)
	{
		// Set the origin and make a list to
		// hold all of the track in the line
		Origin = startPosition;
		Track = new List<CubicBezier>();

		// Set a random debug color for distinguishing
		// different lines against each other
		debugColor = Utils.GetRandomColor();

		//? https://www.desmos.com/calculator/cahqdxeshd
	}

	public void Draw(bool debug = false)
	{
		// Loop through every bit of track in the railway
		foreach (CubicBezier track in Track)
		{
			// Draw it
			Color color = debug ? debugColor : Color.Blue;
			track.Draw(3f, color, debug);
		}
	}

	private Vector2 GetStartPosition()
	{
		// Get where the previous bit of track ended. We will
		// be starting this new part from there so that the
		// two tracks will be connected
		//? the `^1` means the last index. Same as `Track.Count - 1`
		return (Track.Count > 0) ? Track[^1].EndPosition : Origin;
	}

	public Railway AddHorizontalStraight(float length)
	{
		// Get the start position
		Vector2 startPosition = GetStartPosition();

		// Make the new section of track
		Vector2 endPosition = startPosition + new Vector2(length, 0);
		CubicBezier newTrack = new CubicBezier(startPosition, endPosition);

		// Append it to the railway
		Track.Add(newTrack);
		return this;
	}

	public Railway AddVerticalStraight(float length)
	{
		// Get the start position
		Vector2 startPosition = GetStartPosition();

		// Make the new section of track
		Vector2 endPosition = startPosition + new Vector2(0, length);
		CubicBezier newTrack = new CubicBezier(startPosition, endPosition);

		// Append it to the railway
		Track.Add(newTrack);
		return this;
	}

	public Railway AddDownwardsTurn(float length, float height)
	{
		// Get the start position and
		// calculate the end position
		Vector2 startPosition = GetStartPosition();
		Vector2 endPosition = startPosition + new Vector2(length, height);

		// Make the section of track
		CubicBezier newTrack = new CubicBezier(

			startPosition,
			startPosition + new Vector2(length / 1.5f, 0),

			endPosition + new Vector2(0, -height / 1.5f),
			endPosition
		);

		// Append it to the railway
		Track.Add(newTrack);
		return this;
	}

	public Railway AddHorizontalDownwardsPoint(float length)
	{
		// Get the initial start position because
		// it will change when we make the main track
		Vector2 startPosition = GetStartPosition();

		// Make the branching section of the point
		CubicBezier newTrack = new CubicBezier(

			startPosition,
			startPosition + new Vector2(length / 4f, 0f),

			startPosition + new Vector2(length / 2f, length / 4),
			startPosition + new Vector2(length, length / 4)
		);

		// Append it to the railway
		Track.Add(newTrack);
		return this;
	}
}