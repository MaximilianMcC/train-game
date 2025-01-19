using System.Numerics;
using Raylib_cs;

class Spline
{
	private Vector2 point1;
	private Vector2 point2;
	private Vector2 point3;
	private Vector2 point4;
	private float[] arcLengths;

	// Catmull rom spline or something idk
	public Spline(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4, int samples = 100)
	{
		// Assign the points
		this.point1 = point1;
 		this.point2 = point2;
 		this.point3 = point3;
 		this.point4 = point4;

		// Pre-calculate the arc lengths
		arcLengths = ComputeArcLengthTable(samples);
	}

	// Catmull rom spline from another
	//? Used for if they wanna edit this one or something
	// TODO: Don't do this
	public Spline(Spline spline, Vector2 point1Adjustment, Vector2 point2Adjustment, Vector2 point3Adjustment, Vector2 point4Adjustment)
	{
		// Update the adjustments
		point1 = spline.point1 + point1Adjustment;
		point2 = spline.point2 + point2Adjustment;
		point3 = spline.point3 + point3Adjustment;
		point4 = spline.point4 + point4Adjustment;

		// Pre-calculate the arc lengths again
		arcLengths = ComputeArcLengthTable(100);
	}

	// Get the position of a point at the percentage through
	private Vector2 GetPositionAtPercentage(float percentageThroughLine)
	{
		//! idk what any of this does. Best friend locked in
		float t = percentageThroughLine;
		float t2 = t * t;
		float t3 = t2 * t;

		float a0 = -0.5f * t3 + t2 - 0.5f * t;
		float a1 = 1.5f * t3 - 2.5f * t2 + 1.0f;
		float a2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
		float a3 = 0.5f * t3 - 0.5f * t2;

		return a0 * point1 + a1 * point2 + a2 * point3 + a3 * point4;
	}

	// Pre calculate the arc length lookup table
	private float[] ComputeArcLengthTable(int samples)
	{
		//! from the best friend. idk what this does
		float[] arcLengths = new float[samples];
		arcLengths[0] = 0;
		Vector2 previousPoint = GetPositionAtPercentage(0);

		for (int i = 1; i < samples; i++)
		{
			float t = i / (float)(samples - 1);
			Vector2 currentPoint = GetPositionAtPercentage(t);
			arcLengths[i] = arcLengths[i - 1] + Vector2.Distance(previousPoint, currentPoint);
			previousPoint = currentPoint;
		}

		return arcLengths;
	}

	// Get the position evenly spaced
	private float GetNormalizedTForArcLength(float distance)
	{
		//! from the best friend. idk what this does
		for (int i = 1; i < arcLengths.Length; i++)
		{
			if (distance <= arcLengths[i])
			{
				float segmentLength = arcLengths[i] - arcLengths[i - 1];
				float segmentT = (distance - arcLengths[i - 1]) / segmentLength;
				return (i - 1 + segmentT) / (arcLengths.Length - 1);
			}
		}

		return 1;
	}

	public Vector2 GetPosition(float normalizedDistance)
	{
		float t = GetNormalizedTForArcLength(normalizedDistance * arcLengths[^1]);
		return GetPositionAtPercentage(t);
	}

	// Draw the current spline
	public void Draw(Vector2 position, float size, Color color, int pointCount = 100)
	{
		// Figure out how much we gotta increase per
		// iteration because the percentage goes from
		// a number 0-1
		float incrementor = 1f / pointCount;

		// Loop through x times and draw a dot to make the line
		for (float t = 0; t < 1; t += incrementor)
		{
			// Get the position of the current point
			Vector2 currentPointPosition = GetPositionAtPercentage(t);
			Vector2 screenPosition = position + currentPointPosition;

			// Draw it
			Raylib.DrawCircleV(screenPosition, size, color);
		}
	}

}