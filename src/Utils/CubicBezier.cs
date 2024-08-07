using System.Numerics;
using Raylib_cs;

class CubicBezier
{
	public static int Samples = 1000;
	public Vector2 StartPosition { get; private set; }
	public Vector2 EndPosition { get; private set; }
	public float Length { get; private set; }

	private bool curved;
	private Vector2 startControl;
	private Vector2 endControl;

	// Make a cubic bezier thats an actual curve thing
    public CubicBezier(Vector2 startPosition, Vector2 startControlPoint, Vector2 endControlPoint, Vector2 endPosition)
	{
		// Get the start and end position of the curve
		StartPosition = startPosition;
		EndPosition = endPosition;

		// Get the control points that are used to
		// define the shape of the curve
		curved = true;
		startControl = startControlPoint;
		endControl = endControlPoint;

		// Calculate the total length/arc length of the curve
		Length = GetArcLength();
	}

	// Make a cubic bezier thats just a straight line
	public CubicBezier(Vector2 startPosition, Vector2 endPosition)
	{
		// Get the start and end position of the curve
		StartPosition = startPosition;
		EndPosition = endPosition;

		// Set the control points to be the same
		// as the positions. This removes any curvature
		curved = false;
		startControl = startPosition;
		endControl = endPosition;

		// Calculate the total length/arc length
		Length = GetArcLength();
	}

	private Vector2 GetBezierPoint(float range)
	{
		float u = 1 - range;
		float tt = range * range;
		float uu = u * u;
		float ttt = tt * range;
		float uuu = uu * u;
		return (uuu * StartPosition) + (3 * uu * range * startControl) + (3 * u * tt * endControl) + (ttt * EndPosition);
	}

	private float GetArcLength()
	{
		float length = 0f;
		Vector2 prevPoint = StartPosition;

		for (int i = 1; i <= Samples; i++)
		{
			float range = (float)i / Samples;
			Vector2 point = GetBezierPoint(range);
			length += Vector2.Distance(prevPoint, point);
			prevPoint = point;
		}

		return length;
	}

	public float ParameterForArcLength(float distance)
	{
		float length = 0f;
		Vector2 prevPoint = StartPosition;

		for (int i = 1; i <= Samples; i++)
		{
			float range = (float)i / Samples;
			Vector2 point = GetBezierPoint(range);
			length += Vector2.Distance(prevPoint, point);
			if (length >= distance)
			{
				return range;
			}
			prevPoint = point;
		}

		return 1f; // If the distance exceeds the total length, return the end of the curve
	}

	public Vector2 GetPositionFromDistance(float distance)
	{
		distance = ParameterForArcLength(distance);
		return GetBezierPoint(distance);
	}

	public void Draw(float thickness, Color color, bool debug = false)
	{
		// If there is no curve then just draw a
		// line to avoid doing heaps of extra work
		if (curved == false)
		{
			Raylib.DrawLineEx(StartPosition, EndPosition, thickness, color);
			return;
		}

		// TODO: Remove
		if (debug)
		{
			// Draw the start/end points
			Raylib.DrawCircleV(StartPosition, 5f, Color.Green);
			Raylib.DrawCircleV(EndPosition, 5f, Color.Green);

			// Draw lines between the positions and control points
			Raylib.DrawLineEx(StartPosition, startControl, 3f, Color.Orange);
			Raylib.DrawLineEx(EndPosition, endControl, 3f, Color.Orange);

			// Draw the control points
			Raylib.DrawCircleV(startControl, 5f, Color.Red);
			Raylib.DrawCircleV(endControl, 5f, Color.Red);
		}

		// Loop through every sample so that we can
		// draw it with a curve
		for (int i = 0; i <= Samples; i++)
		{
			// Convert the index to be in a range of 0-1
			float distance = (float)i / Samples;
			Vector2 position = GetBezierPoint(distance);

			// Draw a circle at the current position
			Raylib.DrawCircleV(position, (thickness / 2), color);
		}
	}
}
