using System.Numerics;
using Raylib_cs;

class Debug : Prop
{
	private OrientatedBoundingBox boundingBox1;
	private OrientatedBoundingBox boundingBox2;

	public Debug()
	{
		boundingBox1 = new OrientatedBoundingBox(new Vector3(-1, 0, 0), new Vector3(2, 1, 1), 0f);
		boundingBox2 = new OrientatedBoundingBox(new Vector3(1, 0, 0), new Vector3(2, 1, 1), 0f);
	}

	public override void Update()
	{
		// float rotation = 100 * Raylib.GetFrameTime();
		// boundingBox1.Rotation += rotation;
		if (Raylib.IsKeyPressed(KeyboardKey.R))
		{
			boundingBox1.Rotation += 10;
			boundingBox1.UpdateCorners();
		}
	}

	public override void Render3D()
	{
		Color color = boundingBox1.IsCollidingWith(boundingBox2) ? Color.Green : Color.Blue;

		Console.WriteLine(boundingBox1.IsCollidingWith(boundingBox2));

		boundingBox1.Draw(color);
		boundingBox2.Draw(color);
	}
}