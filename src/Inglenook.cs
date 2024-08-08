using System.Numerics;
using Raylib_cs;

class Inglenook
{
	private static Locomotive locomotive;
	private static List<Railway> branchLines;
	private static int branchIndex;

	public static void Start()
	{
		// Make the railway
		float locoLength = 60f;
		float truckLength = 50f;
		branchLines = new List<Railway>()
		{
			// Main line and head shunt thing
			new Railway(new Vector2(50))
				.AddHorizontalStraight(locoLength + (truckLength * 3))
				.AddHorizontalStraight(truckLength * 5),
			
			// First siding
			new Railway(new Vector2(50))
				.AddHorizontalStraight(locoLength + (truckLength * 3))
				.AddHorizontalDownwardsPoint(100f)
				.AddHorizontalStraight(truckLength * 3),

			// Second siding
			new Railway(new Vector2(50))
				.AddHorizontalStraight(locoLength + (truckLength * 3))
				.AddHorizontalDownwardsPoint(100f)
				.AddHorizontalDownwardsPoint(100f)
				.AddHorizontalStraight(truckLength * 3)
		};

		// Make the loco
		locomotive = new Locomotive(branchLines[branchIndex]);
	}

	public static void Update()
	{
		// Check for if we wanna change the points
		if (Raylib.IsKeyPressed(KeyboardKey.Space)) branchIndex++;
		if (branchIndex > branchLines.Count - 1) branchIndex = 0;
		locomotive.Railway = branchLines[branchIndex];

		locomotive.Update();
	}

	public static void Render()
	{
		branchLines.ForEach(line => line.Draw(Game.Debug));
		locomotive.Draw();

		Raylib.DrawText($"{branchIndex}", 100, 100, 30, Color.Black);
	}
}