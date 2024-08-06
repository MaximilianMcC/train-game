using System.Numerics;
using Raylib_cs;

class Inglenook
{
	private static CubicBezier railway;
	private static Train train;

	public static void Start()
	{
		// Make the railway
		Vector2 startPoint = new Vector2(100, 500);
        Vector2 startControl = new Vector2(300, 100);
        Vector2 endPoint = new Vector2(700, 100);
        Vector2 endControl = new Vector2(500, 500);
		railway = new CubicBezier(startPoint, startControl, endPoint, endControl);

		// Make the train
		train = new Train(new List<CubicBezier> { railway });
	}

	public static void Update()
	{
		train.Update();
	}

	public static void Render()
	{
		railway.Draw(5f, Color.Blue);
		train.Render();
	}
}