using Raylib_cs;

class GameManager
{
	// Day stuff
	// TODO: Put in another file/class
	private static float dayLength = 25; //? 1 minute 60
	private static float nightLength = 10; //? quick as 30
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
	}

	public static void CleanUp()
	{
		
	}
}