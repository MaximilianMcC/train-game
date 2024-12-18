//! This is mostly gonna be hardcoded. I reckon its ok
//! for this since its small and not going to change but
//! for anything else a custom map editor would be choice
using System.Numerics;
using Raylib_cs;

class Map
{
	public static Dictionary<string, Model> Models;

	public static void Load()
	{
		// Add all the models, and give them a name
		Models = new Dictionary<string, Model>()
		{
			{ "test", AssetManager.LoadGlbModel("./assets/test.glb") },
			{ "fireplace", AssetManager.LoadGlbModel("./assets/fireplace.glb") },
		};
	}

	public static void Render()
	{
		Raylib.DrawModel(Models["test"], new Vector3(5, 0, 0), 1f, Color.White);
		Raylib.DrawModel(Models["fireplace"], new Vector3(3, 0, 3), 1f, Color.White);
	}

	public static void Unload()
	{
		// Unload everything
		foreach (Model model in Models.Values) Raylib.UnloadModel(model);
	}

	public static unsafe bool Collision(BoundingBox collider)
	{
		// Loop over every mesh of every model
		// TODO: Pre get all of the different meshes in Start() so we don't have to do this every time
		// TODO: Loop over the bounding boxes and check for collisions. If there was a collision then go and check each mesh. Saves you from checking meshes without a reason yk
		foreach (Model model in Models.Values)
		{
			// Get all of the meshes in the model
			Mesh* meshes = model.Meshes;
			int meshCount = model.MeshCount;

			// Loop over every mesh
			for (int i = 0; i < meshCount; i++)
			{
				// Get the mesh and check for
				// if there is any collision
				Mesh mesh = meshes[i];
				BoundingBox boundingBox = Raylib.GetMeshBoundingBox(mesh);
				if (Raylib.CheckCollisionBoxes(collider, boundingBox)) return true;
			}
		}

		// There was no collision
		return false;
	}
}