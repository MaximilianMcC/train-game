using System.Net;
using System.Numerics;
using Raylib_cs;

class Prop : Updatable
{
	public Model Model;
	public BoundingBox BoundingBox;
	public List<BoundingBox> MeshBoundingBoxes;

	public Vector3 Position;
	public Quaternion Rotation;

	public Prop(string modelPath, Vector3 position, float yRotation = 0f)
	{
		// Load the model
		Model = AssetManager.LoadGlbModel(modelPath);
		Model.Transform = Matrix4x4.Identity;

		// Set all the settings and whatnot
		Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(1f);
		Matrix4x4 rotationMatrix = Matrix4x4.CreateRotationY(yRotation);
		Matrix4x4 positionMatrix = Matrix4x4.CreateTranslation(position);

		// Apply the transform to the model
		//? System.Numerics and Raylib have the rows and columns of their matrixes
		//? swapped, so calling transpose on it flips it around so that it all works
		Matrix4x4 matrix = scaleMatrix * rotationMatrix * positionMatrix;
		Matrix4x4 raylibTranslationMatrix = Matrix4x4.Transpose(matrix);
		Model.Transform = raylibTranslationMatrix;
	}

	public override void Render3D()
	{
		// Draw the model
		Raylib.DrawModel(Model, Vector3.Zero, 1f, Color.White);
		Raylib.DrawBoundingBox(BoundingBox, Color.Magenta);

		// foreach (BoundingBox boundingBox in MeshBoundingBoxes)
		// {
		// 	Raylib.DrawBoundingBox(boundingBox, Color.Green);
		// }
	}

	public override void Unload()
	{
		Raylib.UnloadModel(Model);
	}
}