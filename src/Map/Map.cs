//! This is mostly gonna be hardcoded. I reckon its ok
//! for this since its small and not going to change but
//! for anything else a custom map editor would be choice
using System.Numerics;
using Raylib_cs;

class Map
{
	public static Dictionary<string, Prop> Stuff;

	public static void Load()
	{
		// Add all the models, and give them a name
		Stuff = new Dictionary<string, Prop>()
		{
			{ "fireplace", new Prop("./assets/fireplace.glb", new Vector3(3, 0, 3)) }
		};
	}

	public static void Update()
	{
		Stuff["fireplace"].Update();
	}

	public static void Render()
	{
		Stuff["fireplace"].Render3D();
	}

	public static void Unload()
	{
		// Unload everything
		foreach (Prop model in Stuff.Values) model.CleanUp();
	}

	public static unsafe bool Collision(BoundingBox collider)
	{
		// Loop over every object
		foreach (Prop prop in Stuff.Values)
		{
			// First check for if there was any collision at all
			if (Raylib.CheckCollisionBoxes(collider, prop.BoundingBox) == false) continue;

			// If there was collision, then check each mesh
			foreach (BoundingBox boundingBox in prop.MeshBoundingBoxes)
			{
				if (Raylib.CheckCollisionBoxes(collider, boundingBox)) return true;
			}
		}

		// There was no collision
		return false;
	}
}