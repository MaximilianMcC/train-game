using System.Numerics;
using Raylib_cs;

class Inglenook
{
	private static Train train;
	private static Railway railway;

	public static void Start()
	{
		// Make the railway
		railway = new Railway(new Vector2(50f));
		railway.AddHorizontalStraight(300f);
		railway.AddDownwardsTurn(100f, 100f);
		railway.AddVerticalStraight(100f);

		// Make the train
		train = new Train(railway);
	}

	public static void Update()
	{
		train.Update();
	}

	public static void Render()
	{
		railway.Draw(Game.Debug);
		train.Draw();
	}
}