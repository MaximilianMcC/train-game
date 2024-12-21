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
		SetMatrix("main", GetMatrix(position, new Vector3(0, yRotation, 0), 1f));
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

	// TODO: Rename to `GetTransformationMatrix` for clarity
	protected Matrix4x4 GetMatrix(Vector3 position, Vector3 rotation, float scale = 0f)
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

		// Return the matrix
		//? system.numerics and raylib have the rows and columns
		//? flipped, so transposing it flips it all so it works
		return Matrix4x4.Transpose(matrix);
	}

	protected void AddMatrix(string modelName, Matrix4x4 transformationMatrix)
	{
		// First check for if we have a model of that name
		// TODO: Could return bool for if it worked or not idk
		if (Models.ContainsKey(modelName) == false) return;

		// Get the model, apply the matrix, then add it back
		//? Only reason this is done is because Model is a struct I'm pretty sure
		Model model = Models[modelName];
		model.Transform += transformationMatrix;
		Models[modelName] = model;
	}

	//? Legit a one character difference (needed)
	// TODO: Don't copy like this but idk what else I could do
	protected void SetMatrix(string modelName, Matrix4x4 transformationMatrix)
	{
		// First check for if we have a model of that name
		if (Models.ContainsKey(modelName) == false) return;

		// Get the model, apply the matrix, then add it back
		//? Only reason this is done is because Model is a struct I'm pretty sure
		Model model = Models[modelName];
		model.Transform = transformationMatrix;
		Models[modelName] = model;
	}
}