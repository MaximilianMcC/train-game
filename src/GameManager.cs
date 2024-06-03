using System.Numerics;
using Raylib_cs;

class GameManager
{
	// Day stuff
	// TODO: Put in another file/class
	private static float dayLength = 25; //? 1 minute 60
	private static float nightLength = 15; //? quick as 30
	private static float totalCycleLength = dayLength + nightLength;
	private static float currentTime = 0;

	public static void Start()
	{

	}

	public static void Update()
	{
		// Increase the time by however many seconds
		// have passed since the last frame was drawn
		currentTime += Raylib.GetFrameTime();

		// Check for if we need to loop the cycle (next day)
		if (currentTime > totalCycleLength) currentTime = 0;
	}

	public static void Render()
	{
		string timeString = (currentTime < dayLength) ? "Day" : "Night";
 		float remainingTime = (timeString == "Day") ? dayLength - currentTime : nightLength - (currentTime - dayLength);
        float percentage = (remainingTime / ((timeString == "Day") ? dayLength : nightLength)) * 100f;
		Raylib.DrawText($"Time: {currentTime.ToString("F1")} ({timeString})\n\n{percentage.ToString("F")}%", 100, 100, 30, Color.White);


		// Time thingy
		// TODO: No magic numbers
		float width = Game.Size.X - 200f;
		float height = 25f;
		float x = 100f;
		float y = Game.Size.Y - 95f;
		Raylib.DrawRectangleRec(new Rectangle(x, y, width, height), Color.Gold);

		// Cheeky save the initial x for returning to draw
		// the cursor thingy later on
		float initialX = x;

		// Define the colors for the times
		Color nightBackground = Color.Black;
		Color dayBackground = Color.SkyBlue;

		// Calculate the scales of the sections
		float nightScale = (nightLength / totalCycleLength) * width;
		float dayScale = (dayLength / totalCycleLength) * width;

		// Calculate the size for the day
		float dayWidth = (dayLength / totalCycleLength) * width;

		// Calculate the sizes for the full night, and
		// the transition between night and day
		//? Halved because half of it on left, half on right
		float nightWidth = ((nightLength / totalCycleLength) * width) / 2;
		float transitionWidth = dayWidth / 10f;
		nightWidth -= transitionWidth;

		// Draw the left night and transition
		Raylib.DrawRectangleRec(new Rectangle(x, y, nightWidth, height), nightBackground);
		x += nightWidth;
		Raylib.DrawRectangleGradientEx(new Rectangle(x, y, transitionWidth, height), nightBackground, nightBackground, dayBackground, dayBackground);
		x += transitionWidth;

		// Draw the day
		Raylib.DrawRectangleRec(new Rectangle(x, y, dayWidth, height), dayBackground);
		x += dayWidth;

		// Draw the transition and right night
		Raylib.DrawRectangleGradientEx(new Rectangle(x, y, transitionWidth, height), dayBackground, dayBackground, nightBackground, nightBackground);
		x += transitionWidth;
		Raylib.DrawRectangleRec(new Rectangle(x, y, nightWidth, height), nightBackground);

		// Draw the little pointer cursor arrow thingy that
		// shows what the current time is
		x = initialX + (currentTime / totalCycleLength) * width;
		Raylib.DrawLineEx(new Vector2(x, y), new Vector2(x, height), 5f, Color.Beige);
	}

	public static void CleanUp()
	{
		
	}
}