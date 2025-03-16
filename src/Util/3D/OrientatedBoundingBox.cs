using System.Numerics;
using Raylib_cs;

class OrientatedBoundingBox
{
	public Vector3 Position;
	public float Rotation;
	public Vector3 Scale;
	public Vector3[] CornerPositions;

	public OrientatedBoundingBox(Vector3 position, Vector3 scale, float yRotation)
	{
		Position = position;
		Rotation = yRotation;
		Scale = scale;

		CornerPositions = new Vector3[8];
		UpdateCorners();
	}

	public void UpdateCorners(float rotationToAdd = 0)
	{
		// Quick shortcut thing idk
		Rotation += rotationToAdd;

		// Precalculate half of the box size
		// because the centre is half of the total
		// and all the measurements/maths is done
		// from the centre of the box
		Vector3 halfSize = Scale / 2;

		// Get the direction/matrix for the rotation
		//? Y axis only rn
		float cos = MathF.Cos(Rotation * Raylib.DEG2RAD);
		float sin = MathF.Sin(Rotation * Raylib.DEG2RAD);

		// Get all of the corners of the box, not
		// including rotation or anything. Just a normal
		// axis aligned bounding box.
		Vector3[] localCorners = new Vector3[]
		{
			new Vector3(-halfSize.X, -halfSize.Y, -halfSize.Z),
			new Vector3(halfSize.X, -halfSize.Y, -halfSize.Z),
			new Vector3(halfSize.X, -halfSize.Y, halfSize.Z),
			new Vector3(-halfSize.X, -halfSize.Y, halfSize.Z),
			new Vector3(-halfSize.X, halfSize.Y, -halfSize.Z),
			new Vector3(halfSize.X, halfSize.Y, -halfSize.Z),
			new Vector3(halfSize.X, halfSize.Y, halfSize.Z),
			new Vector3(-halfSize.X, halfSize.Y, halfSize.Z)			
		};

		// Add the rotation to make the axis aligned
		// bounding box an orientated bounding box
		// TODO: Use proper matrixes for this
		for (int i = 0; i < 8; i++)
		{
			// Rotate the current point
			float x = (localCorners[i].X * cos) - (localCorners[i].Z * sin);
			float z = (localCorners[i].X * sin) + (localCorners[i].Z * cos);

			// Put it into the new corner
			CornerPositions[i] = new Vector3(x, localCorners[i].Y, z) + Position;
		}
	}

	public bool IsCollidingWith(OrientatedBoundingBox other)
	{
		// Get all the separated axes between
		// the two bounding boxes
		Vector3[] axes = GetAxes(other);

		// Loop over all axes and check for if
		// there are any that aren't touching.
		// If one isn't touching there is no
		// collision across the whole thing.
		foreach (Vector3 axis in axes)
		{
			// If they touch then break early
			if (IsAxisTouching(other, axis) == false) return false;
		}

		// There was a collision
		return true;
	}

	private bool IsAxisTouching(OrientatedBoundingBox other, Vector3 axis)
	{
		// "squash"/project ourself onto the graph
		float minA = float.MaxValue;
		float maxA = float.MinValue;
		foreach (Vector3 corner in CornerPositions)
		{
			// Make them 2D if that makes any sense
			float projection = Vector3.Dot(corner, axis);
			minA = MathF.Min(minA, projection);
			maxA = MathF.Max(maxA, projection);
		}

		// "squash"/project the other box onto the graph
		float minB = float.MaxValue;
		float maxB = float.MinValue;
		foreach (Vector3 corner in other.CornerPositions)
		{
			// Make them 2D if that makes any sense
			float projection = Vector3.Dot(corner, axis);
			minB = MathF.Min(minB, projection);
			maxB = MathF.Max(maxB, projection);
		}

		// If we're either on the very left
		// or very right then the axes do
		// not collide, and there is no
		// collision for this particular axis
		return !(maxA < minB || maxB < minA);
	}

	private Vector3[] GetAxes(OrientatedBoundingBox b)
	{
		OrientatedBoundingBox a = this;

		return new Vector3[]
		{
			GetEdgeAxis(a.CornerPositions[0], a.CornerPositions[1]), // Local X-axis of OBB A
			GetEdgeAxis(a.CornerPositions[0], a.CornerPositions[3]), // Local Z-axis of OBB A
			GetEdgeAxis(a.CornerPositions[0], a.CornerPositions[4]), // Local Y-axis of OBB A
			GetEdgeAxis(b.CornerPositions[0], b.CornerPositions[1]), // Local X-axis of OBB B
			GetEdgeAxis(b.CornerPositions[0], b.CornerPositions[3]), // Local Z-axis of OBB B
			GetEdgeAxis(b.CornerPositions[0], b.CornerPositions[4])  // Local Y-axis of OBB B
		};
	}

	private Vector3 GetEdgeAxis(Vector3 position1, Vector3 position2)
	{
		return Vector3.Normalize(position2 - position1);
	}

	public void Draw(Color color)
	{
		// TODO: Don't do this way (lazy)
		for (int i = 0; i < CornerPositions.Length; i++)
		{
			for (int j = CornerPositions.Length - 1; j >= 0 ; j--)
			{
				Raylib.DrawLine3D(CornerPositions[i], CornerPositions[j], color);
			}
		}
	}
}