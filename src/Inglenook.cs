using System.Numerics;
using Raylib_cs;

class Inglenook
{
	private static Locomotive locomotive;
	private static Railway railway;

	public static void Start()
	{
		// Make the railway
		railway = new Railway(new Vector2(50f));
		railway.AddHorizontalStraight(300f);
		railway.AddHorizontalDownwardsPoint(100f);
		railway.AddHorizontalStraight(100f);

		// Make the loco
		locomotive = new Locomotive(railway);
	}

	public static void Update()
	{
		locomotive.Update();
	}

	public static void Render()
	{
		railway.Draw(Game.Debug);
		locomotive.Draw();
	}
}