using System.Numerics;
using Raylib_cs;

class CatmullRomSpline
{
	public float Length;
	private List<Vector2> sampledPoints;
	private List<float> arcLengths;
	private int samples = 1024;

	public CatmullRomSpline(List<Vector2> points)
	{
		// Convert the raw control points to
		// processed ones so that we can calculate
		// distance at a linear rate change thingy
		sampledPoints = SamplePoints(points, samples, out arcLengths);
		Length = arcLengths[arcLengths.Count - 1];
	}

	public void Draw(float thickness, Color color)
	{
		// Loop over every point
		for (int i = 0; i < sampledPoints.Count - 1; i++)
		{
			// Draw it
			Raylib.DrawLineEx(sampledPoints[i], sampledPoints[i + 1], thickness, color);
		}
	}

	private List<Vector2> SamplePoints(List<Vector2> points, int samples, out List<float> arcLengths)
	{
		List<Vector2> sampledPoints = new List<Vector2>();
		arcLengths = new List<float>();
		float totalLength = 0;

		for (int i = 0; i < points.Count - 3; i++)
		{
			for (float t = 0; t <= 1; t += 1.0f / samples)
			{
				Vector2 p0 = points[i];
				Vector2 p1 = points[i + 1];
				Vector2 p2 = points[i + 2];
				Vector2 p3 = points[i + 3];

				Vector2 point = CatmullRom(p0, p1, p2, p3, t);
				sampledPoints.Add(point);

				if (sampledPoints.Count > 1)
				{
					totalLength += Vector2.Distance(sampledPoints[sampledPoints.Count - 2], point);
					arcLengths.Add(totalLength);
				}
				else
				{
					arcLengths.Add(0);
				}
			}
		}

		return sampledPoints;
	}

	private Vector2 CatmullRom(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float t2 = t * t;
		float t3 = t2 * t;

		float a0 = -0.5f * t3 + t2 - 0.5f * t;
		float a1 =  1.5f * t3 - 2.5f * t2 + 1.0f;
		float a2 = -1.5f * t3 + 2.0f * t2 + 0.5f * t;
		float a3 =  0.5f * t3 - 0.5f * t2;

		float x = a0 * p0.X + a1 * p1.X + a2 * p2.X + a3 * p3.X;
		float y = a0 * p0.Y + a1 * p1.Y + a2 * p2.Y + a3 * p3.Y;

		return new Vector2(x, y);
	}

	public Vector2 GetPositionFromPercentage(List<Vector2> points, float percentage)
	{
		int index = arcLengths.BinarySearch(percentage);
		if (index < 0) index = ~index;

		if (index == 0) return points[0];
		if (index >= points.Count) return points[points.Count - 1];

		float segmentLength = arcLengths[index] - arcLengths[index - 1];
		float t = (percentage - arcLengths[index - 1]) / segmentLength;

		return Vector2.Lerp(points[index - 1], points[index], t);
	}
}