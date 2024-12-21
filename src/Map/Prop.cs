using System.Net;
using System.Numerics;
using Raylib_cs;

class Prop : Updatable
{
	public Dictionary<string, Model> Models = [];

	//? Empty constructor for custom stuff idk
	public Prop() {}

	public Prop(string modelPath, Vector3 position, float yRotation = 0f)
	{
		// Load in a default "main" model thingy idk
		Models.Add("main", AssetManager.LoadGlbModel(modelPath));
		SetModelTransform("main", position, new Vector3(0, yRotation, 0));
	}

	public override void Render3D()
	{
		// Draw everything
		foreach (Model model in Models.Values) Raylib.DrawModel(model, Vector3.Zero, 1f, Color.White);
	}

	public override void Unload()
	{
		// Unload all the models
		foreach (Model model in Models.Values) Raylib.UnloadModel(model);
	}

	//? Not using a reference doesn't work for some reason. I swear I gotta relearn how to code bruh
	protected void SetModelTransform(string modelName, Vector3 position, Vector3 rotation, float scale = 1f)
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
		//? gotta do all this rinky dictionary stuff because model is a struct, not a class
		Model model = Models[modelName];
		model.Transform = Matrix4x4.Transpose(matrix);
		Models[modelName] = model;
	}
}