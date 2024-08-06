using System.Numerics;
using Raylib_cs;

class CubicBezier
{
	public static int Samples = 1000;
	public Vector2 StartPosition;
	public Vector2 EndPosition;
	public float ArcLength;

	private Vector2 startControl;
	private Vector2 endControl;

	public CubicBezier(Vector2 startPosition, Vector2 startControlPoint, Vector2 endPosition, Vector2 endControlPoint)
	{
		// Get the start and end position of the curve
		StartPosition = startPosition;
		EndPosition = endPosition;

		// Get the control points that are used to
		// define the shape of the curve
		startControl = startControlPoint;
		endControl = endControlPoint;

		// Calculate the total length/arc length of the curve
		ArcLength = GetArcLength();
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

	public void Draw(float thickness, Color color)
	{
		// Loop through every sample
		for (int i = 0; i <= Samples; i++)
		{
			// Convert the index to be in a range of 0-1
			float distance = (float)i / Samples;
			Vector2 position = GetBezierPoint(distance);

			// Draw a circle at the current position
			Raylib.DrawCircleV(position, thickness, color);
		}
	}
}
