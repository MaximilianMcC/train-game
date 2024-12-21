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
			// Actual signal box
			{ "room", new Prop("./assets/room.glb", new Vector3(0, -0.01f, 0)) },

			// Furniture and stuff
			{ "fireplace", new Prop("./assets/fireplace.glb", new Vector3(-3f, 0, 1.5f), -230) },
			{ "painting", new Prop("./assets/painting.glb", new Vector3(-2, 1.8f, 2), 180)},
			{ "clock", new Clock(new Vector3(2f, 1.6f, 2), 180) },
			{ "desk", new Prop("./assets/desk.glb", new Vector3(2.95f, 0f, 1.65f)) },

			// idk
			{ "debug", new Debug() }
		};
	}

	public static void Update()
	{
		foreach (Prop prop in Stuff.Values) prop.Update();
	}

	public static void Render()
	{
		foreach (Prop prop in Stuff.Values) prop.Render3D();
	}

	public static void Unload()
	{
		// Unload everything
		foreach (Prop prop in Stuff.Values) prop.Unload();
	}

	public static unsafe bool Collision(BoundingBox collider)
	{
		// Loop over every object
		foreach (Prop prop in Stuff.Values)
		{
			// First check for if there was any collision at all
			// if (Raylib.CheckCollisionBoxes(collider, prop.BoundingBox) == false) continue;

			// If there was collision, then check each mesh
			// foreach (BoundingBox boundingBox in prop.MeshBoundingBoxes)
			// {
			// 	if (Raylib.CheckCollisionBoxes(collider, boundingBox)) return true;
			// }
		}

		// There was no collision
		return false;
	}
}