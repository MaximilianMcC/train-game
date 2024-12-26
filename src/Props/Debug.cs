using System.Numerics;
using Raylib_cs;

class Debug : Prop
{
	public Debug() : base("./assets/debug.glb", new Vector3(0, 0, -10)) {}

	public override void Update()
	{
		float rotation = 100 * Raylib.GetFrameTime();

		AddMatrix(
			Models.FirstOrDefault().Key,
			GenerateMatrix(
				Vector3.Zero,
				new Vector3(0, rotation, 0),
				1
			)
		);
	}
}