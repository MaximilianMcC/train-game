using System.Numerics;
using Raylib_cs;

class Inglenook
{
	private static List<CubicBezier> railway;
	private static Train train;

	public static void Start()
	{
		// Make the railway
		railway = new List<CubicBezier>()
		{
			// Straight bit
			new CubicBezier(
				new Vector2(50, 50),
				new Vector2(350, 50)
			),

			// Curve bit
			new CubicBezier(
				new Vector2(350, 50),
				new Vector2(600, 50),
				new Vector2(700, 500),
				new Vector2(700, 500)
			)
			// new CubicBezier(
			// 	new Vector2(100, 500),
			// 	new Vector2(300, 100),
			// 	new Vector2(700, 100),
			// 	new Vector2(500, 500)
			// )
		};

		// Make the train
		train = new Train(railway);
	}

	public static void Update()
	{
		train.Update();
	}

	public static void Render()
	{
		// Loop over every bit of track
		// in the railway and draw it
		foreach (CubicBezier trackPart in railway)
		{
			trackPart.Draw(3f, Color.Blue);
		}

		train.Render();
	}
}