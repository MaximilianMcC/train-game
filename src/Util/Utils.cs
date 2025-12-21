using Raylib_cs;

class Utils
{
	public static Color RandomColor()
	{
		Random random = new Random();
		return new Color(
			random.Next(0, 255),
			random.Next(0, 255),
			random.Next(0, 255),
			255
		);
	}
}