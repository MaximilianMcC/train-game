using System.Net;
using System.Numerics;
using Raylib_cs;

class Prop : Updatable
{
	public Model Model;
	public BoundingBox BoundingBox;
	public List<BoundingBox> MeshBoundingBoxes;

	public Prop() { }

	public Prop(string modelPath, Vector3 position, float yRotation = 0f)
	{
		// Load the model
		Model = AssetManager.LoadGlbModel(modelPath);
		SetModelTransform(ref Model, position, new Vector3(0, yRotation, 0));
	}

	public override void Render3D()
	{
		// Draw the model
		Raylib.DrawModel(Model, Vector3.Zero, 1f, Color.White);
	}

	public override void Unload()
	{
		Raylib.UnloadModel(Model);
	}

	//? Not using a reference doesn't work for some reason. I swear I gotta relearn how to code bruh
	protected void SetModelTransform(ref Model model, Vector3 position, Vector3 rotation, float scale = 1f)
	{
		// Set the position and scale
		Matrix4x4 positionMatrix = Matrix4x4.CreateTranslation(position);
		Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);

		// Set the rotation
		// TODO: Do in a one-liner
		Matrix4x4 rotationX = Matrix4x4.CreateRotationX(rotation.X * Raylib.DEG2RAD);
		Matrix4x4 rotationY = Matrix4x4.CreateRotationY(rotation.Y * Raylib.DEG2RAD);
		Matrix4x4 rotationZ = Matrix4x4.CreateRotationZ(rotation.Z * Raylib.DEG2RAD);
		Matrix4x4 rotationMatrix = rotationX * rotationY * rotationZ;

		// Combine everything
		//! Values must be multiplied in this order
		Matrix4x4 matrix = scaleMatrix * rotationMatrix * positionMatrix;

		// Transpose the matrix then apply it to the model
		//? system.numerics and raylib have the rows and columns
		//? flipped, so transposing it flips it all so it works
		model.Transform = Matrix4x4.Transpose(matrix);
	}
}