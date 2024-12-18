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

	public Prop(string modelPath, Vector3 position)
	{
		// Load in the model
		Model = AssetManager.LoadGlbModel(modelPath);

		// Set the position and whatnot
		Position = position;
		UpdateBoundingBoxes();
	}

	//? This is in a method because unless the thing is moving,
	//? then the bounding box will kinda never move I think yk
	private unsafe void UpdateBoundingBoxes()
	{
		// First modify the whole "generalised" bounding box
		BoundingBox modelBoundingBox = Raylib.GetModelBoundingBox(Model);
		BoundingBox = new BoundingBox(Position + modelBoundingBox.Min, Position + modelBoundingBox.Max);

		// Get the mesh data (unsafe)
		Mesh* meshes = Model.Meshes;
		int meshCount = Model.MeshCount;

		// Recalculate every mesh bounding box, and also
		// chuck it all in a list so we don't have to deal
		// with all this dodgy as unsafe array pointer stuff 
		// TODO: Clear the list instead of making a new one
		MeshBoundingBoxes = new List<BoundingBox>();
		for (int i = 0; i < meshCount; i++)
		{
			// Get the bounding box, and add the new position
			BoundingBox meshBoundingBox = Raylib.GetMeshBoundingBox(meshes[i]);
			BoundingBox adjustedBoundingBox = new BoundingBox(Position + meshBoundingBox.Min, Position + meshBoundingBox.Max);
			MeshBoundingBoxes.Add(adjustedBoundingBox);
		}
	}

	public override void Render3D()
	{
		// Draw the model
		Raylib.DrawModel(Model, Position, 1f, Color.White);
		Raylib.DrawBoundingBox(BoundingBox, Color.Magenta);

		foreach (BoundingBox boundingBox in MeshBoundingBoxes)
		{
			Raylib.DrawBoundingBox(boundingBox, Color.Green);
		}
	}

	public override void CleanUp()
	{
		Raylib.UnloadModel(Model);
	}
}