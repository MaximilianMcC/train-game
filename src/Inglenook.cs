using System.Numerics;
using Raylib_cs;

class Inglenook
{
	
	private static Bezier bezier1 = new Bezier(new Vector2(100, 300), new Vector2(400, 300), new Vector2(700, 500));
	private static Bezier bezier2 = new Bezier(new Vector2(100, 300), new Vector2(500, 300), new Vector2(700, 300));

	public static void Start()
	{

	}

	public static void Update()
	{

	}

	public static void Render()
	{
		bezier1.Draw(10, Color.Black);
		bezier2.Draw(10, Color.Magenta);
	}
}