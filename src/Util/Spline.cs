using System.Numerics;

class Spline
{
	public Vector2 Point1 { get; set; }
	public Vector2 Point2 { get; set; }
	public Vector2 Point3 { get; set; }
	public Vector2 Point4 { get; set; }

	// Catmull rom spline or something idk
	public Spline(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
	{
		// Assign the points
		Point1 = point1;
 		Point2 = point2;
 		Point3 = point3;
 		Point4 = point4;
	}

	// Represents the position on the line and is from 0.0 - 1.0
	public Vector2 GetPositionAtPercentage(float percentageThroughLine)
	{
		return new Vector2(
			GetPositionComponent(true, percentageThroughLine),
			GetPositionComponent(false, percentageThroughLine)
		);
	}

	//! idk what any of this does. Shout out best friend
	private float GetPositionComponent(bool x, float percentage)
	{
		// Get like positions through the spline?
		float t = percentage;
		float t2 = t * t;
    	float t3 = t2 * t;

		// Figure out what component we're using
		//? x, or y
		float p0 = x ? Point1.X : Point1.Y;
		float p1 = x ? Point2.X : Point2.Y;
		float p2 = x ? Point3.X : Point3.Y;
		float p3 = x ? Point4.X : Point4.Y;

		// Phat bit of maths from the best friend
		float value = 0.5f * ((2 * p1) +
					(-p0 + p2) * t +
					(2 * p0 - 5 * p1 + 4 * p2 - p3) * t2 +
					(-p0 + 3 * p1 - 3 * p2 + p3) * t3);

		// Chuck it back
		return value;
	}
}