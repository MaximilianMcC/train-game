using System.Numerics;
using Raylib_cs;

class GameManager
{
	// Day stuff
	// TODO: Put in another file/class
	public static float DayLength { get; private set; } = 25; //? 1 minute 60
	public static float NightLength { get; private set; } = 15; //? quick as 30
	public static float TotalCycleLength { get; private set; } = DayLength + NightLength;
	public static float CurrentTime { get; private set; } = 0f;

	public static void Start()
	{

	}

	public static void Update()
	{
		// Increase the time by however many seconds
		// have passed since the last frame was drawn
		CurrentTime += Raylib.GetFrameTime();

		// Check for if we need to loop the cycle (next day)
		if (CurrentTime > TotalCycleLength) CurrentTime = 0;
	}

	public static void Render()
	{
		string timeString = (CurrentTime < DayLength) ? "Day" : "Night";
 		float remainingTime = (timeString == "Day") ? DayLength - CurrentTime : NightLength - (CurrentTime - DayLength);
		float percentage = (remainingTime / ((timeString == "Day") ? DayLength : NightLength)) * 100f;
		Raylib.DrawText($"Time: {CurrentTime.ToString("F1")} ({timeString})\n\n{percentage.ToString("F")}%", 100, 100, 30, Color.White);


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
		float nightScale = (NightLength / TotalCycleLength) * width;
		float dayScale = (DayLength / TotalCycleLength) * width;

		// Calculate the size for the day
		float dayWidth = (DayLength / TotalCycleLength) * width;

		// Calculate the sizes for the full night, and
		// the transition between night and day
		//? Halved because half of it on left, half on right
		float nightWidth = ((NightLength / TotalCycleLength) * width) / 2;
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
		x = initialX + (CurrentTime / TotalCycleLength) * width;
		Raylib.DrawLineEx(new Vector2(x, y), new Vector2(x, y + height), 5f, Color.Beige);
	}

	public static void DrawTimeOverlay()
	{
		// Make a dark overlay to put over everything and change
		// its opacity depending on the time to give the illusion
		// of the sun/moon rising and setting (dark/light)
		byte alpha = (byte)((GameManager.CurrentTime * byte.MaxValue) / GameManager.TotalCycleLength);
		Color timeColor = new Color(byte.MinValue, byte.MinValue, byte.MinValue, alpha);

		// Draw the time color rectangle
		Raylib.DrawRectangleV(Vector2.Zero, Game.Size, timeColor);
	}

	public static void CleanUp()
	{
		
	}
}