using System.Numerics;
using Raylib_cs;

class Bezier
{
	public Vector2 StartPosition;
	public Vector2 MiddlePosition;
	public Vector2 EndPosition;

	// Make a bezier with a middle part (turn)
	public Bezier(Vector2 startPosition, Vector2 middlePosition, Vector2 endPosition)
	{
		// Assign values
		StartPosition = startPosition;
		MiddlePosition = middlePosition;
		EndPosition = endPosition;
	}

	// Make a bezier without a middle part (straight)
	public Bezier(Vector2 startPosition, Vector2 endPosition)
	{
		// Assign values and make the middle position
		// the middle of the two given positions
		StartPosition = startPosition;
		MiddlePosition = (startPosition + endPosition) / 2;
		EndPosition = endPosition;
	}

	// idk what any of this does but it works trust
	// shout out to the best friend for writing it
	// I write the banger comments though trust
	public Vector2 GetPositionFromPoint(float positionAlongLine)
	{
		// Do heaps of maths
		float u = 1 - positionAlongLine;
		float tt = positionAlongLine * positionAlongLine;
		float uu = u * u;

		// More maths and get a position from it
		Vector2 position = uu * StartPosition;
		position += 2 * u * positionAlongLine * MiddlePosition;
		position += tt * EndPosition;

		// Give back the position
		return position;
	}

	// Draw a bezier with a default set amount of points
	public void Draw(float thickness, Color color)
	{
		int points = 128;
		Draw(thickness, points, color);
	}

	public void Draw(float thickness, int points, Color color)
	{
		// Check for if the bezier is just a straight line. If it is then
		// skip all the fancy rendering we're about to do and just straight
		// up draw a line
		bool straight = (StartPosition == MiddlePosition || StartPosition == EndPosition || MiddlePosition == EndPosition) ||
			(StartPosition.X == MiddlePosition.X && MiddlePosition.X == EndPosition.X) ||
			(StartPosition.Y == MiddlePosition.Y && MiddlePosition.Y == EndPosition.Y);

		if (straight)
		{
			// Just draw a normal line
			Raylib.DrawLineEx(StartPosition, EndPosition, thickness, color);
			return;
		}

		// Otherwise, turn the number of points into a number from
		// 0 to 1 then trace the path of the bezier along its path
		// and draw circles there
		float incrementor = 1f / (points - 1);
		for (float i = 0f; i <= 1; i += incrementor)
		{
			Vector2 circlePosition = GetPositionFromPoint(i);
			Raylib.DrawCircleV(circlePosition, thickness / 2, color);
		}
	}
}