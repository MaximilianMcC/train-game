using System.Net;
using System.Numerics;
using Raylib_cs;

class Prop : Updatable
{
	public Dictionary<string, Model> Models = [];
	public List<BoundingBox> Hitboxes = [];
	public Vector3 Position;

	//? Empty constructor for custom stuff idk
	public Prop() {}

	//? Lazy ctor for if theres nothing special about the thing
	public Prop(string modelPath, Vector3 position, float yRotation = 0f)
	{
		// Set the position
		Position = position;

		// Load in a default "main" model thingy idk
		Models.Add("main", AssetManager.LoadGlbModel(modelPath));
		SetMatrix("main", GenerateMatrix(Position, new Vector3(0, yRotation, 0), 1f));

	}

	public override void Render3D()
	{
		// Draw everything
		foreach (Model model in Models.Values)
		{
			Raylib.DrawModel(model, Vector3.Zero, 1f, Color.White);
		}
	}

	public override void Unload()
	{
		// Unload all the models
		foreach (Model model in Models.Values) Raylib.UnloadModel(model);
	}

	// TODO: Rename to `GenerateTransformationMatrix` for clarity
	protected Matrix4x4 GenerateMatrix(Vector3 position, Vector3 rotation, float scale = 1)
	{
		// Set the position and scale
		Matrix4x4 positionMatrix = Matrix4x4.CreateTranslation(position);
		Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);

		// Set the rotation
		//? the `% 360` bit normalises the rotations. This stops it from flipping inside out
		Quaternion quaternion = Quaternion.CreateFromYawPitchRoll(
			(rotation.Y % 360) * Raylib.DEG2RAD, 
			(rotation.X % 360) * Raylib.DEG2RAD, 
			(rotation.Z % 360) * Raylib.DEG2RAD
		);
		Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(quaternion);

		// Combine everything
		//! Values must be multiplied in this order
		// Matrix4x4 matrix = positionMatrix * rotationMatrix * scaleMatrix;
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

		// Since we've updated the model, also update
		// the hitboxes for the model/meshes
		UpdateBoundingBoxes(model.Transform);
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

		// Since we've updated the model, also update
		// the hitboxes for the model/meshes
		UpdateBoundingBoxes(model.Transform);
	}

	// TODO: Make safe somehow idk
	private unsafe void UpdateBoundingBoxes(Matrix4x4 transformation)
	{
		// Clear the hitboxes since we're
		// gonna be regenerating them all
		Hitboxes.Clear();

		// Loop over all models
		foreach (Model model in Models.Values)
		{
			// Get all of the meshes for the current model (unsafe)
			Mesh* meshes = model.Meshes;
			int meshCount = model.MeshCount;

			// Loop over every mesh and add its bounding box
			// to the new list of hitboxes
			for (int i = 0; i < meshCount; i++)
			{
				// Get the bounding box that is centred at the world/models origin
				BoundingBox originalBoundingBox = Raylib.GetMeshBoundingBox(meshes[i]);

				// Extract the translation only
				Vector3 translation = new Vector3(transformation.M14, transformation.M24, transformation.M34);

				// Update the position of the bounding box according to a transformation matrix
				//? this is ONLY the position. Raylib doesn't support the other stuff
				// TODO: Manually implement scale
				// TODO: Submit an issue to add rotation support, or use a library for it
				Vector3 transformedMin = originalBoundingBox.Min + translation;
				Vector3 transformedMax = originalBoundingBox.Max + translation;
				BoundingBox transformedBoundingBox = new BoundingBox(transformedMin, transformedMax);

				// Add the new hitbox to the list, and
				// also apply it to the model
				Hitboxes.Add(transformedBoundingBox);
			}
		}
	}

}