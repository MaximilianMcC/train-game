class Program
{
	public static void Main(string[] args)
	{
		// Toggle debug mode by launching with -d
		if (args.Length >= 1 && args[0].ToLower() == "-d") Game.Debug = true;

		// Run the game
		Game.Run();
	}
}