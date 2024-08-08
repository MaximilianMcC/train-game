using System.Numerics;
using Raylib_cs;

class Utils
{
	// Convert a number from 0-1 to a number between x-y
	public static float ConvertRange(float number, float min, float max)
	{
		return max - (number * (max - min));
	}

	// Draw a line at an angle from a base
	public static void DrawAngledLine(Vector2 startPosition, float length, float angle, float thickness, Color color)
	{
		// Convert the angle from degrees to radians
		float angleRadians = angle * Raylib.DEG2RAD;

		// Get the position of the end point
		Vector2 endPosition = new Vector2(
			startPosition.X + length * MathF.Cos(angleRadians),
			startPosition.Y - length * MathF.Sin(angleRadians)
		);

		// Draw the line
		Raylib.DrawLineEx(startPosition, endPosition, thickness, color);
	}

	// Draw a render texture (flip it and whatnot)
	public static void DrawRenderTexture(RenderTexture2D renderTexture, Vector2 position)
	{
		Raylib.DrawTexturePro(
			renderTexture.Texture,
			new Rectangle(0, 0, renderTexture.Texture.Width, -renderTexture.Texture.Height),
			new Rectangle(position, renderTexture.Texture.Width, renderTexture.Texture.Height),
			Vector2.Zero, 0f,
			Color.White
		);
	}

	public static Color GetRandomColor()
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