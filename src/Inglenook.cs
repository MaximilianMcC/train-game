using System.Numerics;
using Raylib_cs;

class Inglenook
{
	private static CatmullRomSpline test;

	public static void Start()
	{
		List<Vector2> points = new List<Vector2>()
		{
			new Vector2(100, 300),
			new Vector2(200, 500),
			new Vector2(400, 200),
			new Vector2(600, 400),
			new Vector2(700, 100)
		};
		test = new CatmullRomSpline(points);
	}

	public static void Update()
	{
		
	}

	public static void Render()
	{
		test.Draw(5f, Color.Blue);
	}
}