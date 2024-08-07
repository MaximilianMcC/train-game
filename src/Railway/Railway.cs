using System.Numerics;
using Raylib_cs;

class Railway
{
	public Vector2 Origin { get; private set; }
	public List<CubicBezier> Track { get; private set; }
	public bool ClosedLoop { get; private set; }

	public Railway(Vector2 startPosition)
	{
		Origin = startPosition;
		Track = new List<CubicBezier>();

		//? https://www.desmos.com/calculator/cahqdxeshd
	}

	public void Draw(bool debug = false)
	{
		// Loop through every bit of track in the railway
		foreach (CubicBezier track in Track)
		{
			// Draw it
			track.Draw(3f, Color.Blue, debug);
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

	public void AddHorizontalStraight(float length)
	{
		// Get the start position
		Vector2 startPosition = GetStartPosition();

		// Make the new section of track
		Vector2 endPosition = startPosition + new Vector2(length, 0);
		CubicBezier newTrack = new CubicBezier(startPosition, endPosition);

		// Append it to the railway
		Track.Add(newTrack);
	}

	public void AddVerticalStraight(float length)
	{
		// Get the start position
		Vector2 startPosition = GetStartPosition();

		// Make the new section of track
		Vector2 endPosition = startPosition + new Vector2(0, length);
		CubicBezier newTrack = new CubicBezier(startPosition, endPosition);

		// Append it to the railway
		Track.Add(newTrack);
	}

	public void AddDownwardsTurn(float length, float height)
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
	}
}