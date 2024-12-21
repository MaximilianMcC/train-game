using System.Numerics;
using Raylib_cs;

class Debug : Prop
{
	public Debug() : base("./assets/debug.glb", Vector3.Zero) {}

	public override void Update()
	{
		if (!Raylib.IsKeyPressed(KeyboardKey.Space)) return;

		AddMatrix("main", GetMatrix(Vector3.Zero, Vector3.Zero, 1.1f));
	}
}