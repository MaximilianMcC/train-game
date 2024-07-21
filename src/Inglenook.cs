using System.Numerics;
using Raylib_cs;

class Inglenook
{
	
	private static List<Bezier> railway;
	private static Train train;


	public static void Start()
	{
		// Make a new railway
		railway = new List<Bezier>()
		{
			new Bezier(new Vector2(100, 300), new Vector2(300, 300)),
			new Bezier(new Vector2(300, 300), new Vector2(500, 300), new Vector2(700, 500))
		};

		// Chuck a train on it
		train = new Train(railway);
	}

	public static void Update()
	{
		train.Update();
	}

	public static void Render()
	{
		// Draw all the track
		foreach (Bezier track in railway)
		{
			track.Draw(10f, Color.Brown);
		}

		// Draw the train
		train.Render();
	}
}