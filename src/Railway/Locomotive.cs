using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Locomotive : ThingOnTrack
{
	private RenderTexture2D footplate;
	private float regulator = 0;
	private bool reversing;

	public Locomotive(Railway railway, int trackIndex = 0) : base(railway, trackIndex)
	{
		// Make the render texture to draw the footplate on
		footplate = Raylib.LoadRenderTexture(250, 250);
	}

	public override void Update()
	{
		// Do all the track related stuff idk
		base.Update();

		Footplate();
	}

	// Controlling the train
	private void Footplate()
	{
		// Check for if we want to open/close the regulator
		float regulatorSpeed = 0.5f * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) regulator -= regulatorSpeed;
		if (Raylib.IsKeyDown(KeyboardKey.Right)) regulator += regulatorSpeed;

		// Clamp the regulator to a value between 0-1
		regulator = Math.Clamp(regulator, 0, 1);

		// Figure out what the current speed should be based
		// on how much steam the regulator is letting through
		float speed = (400 * regulator) * Raylib.GetFrameTime();
		
		// Check for if we're in reversing gear or not. To switch
		// gears the train must be stopped
		//? idk if this is actually accurate because I swear you can change it while moving
		if (speed == 0 && Raylib.IsKeyPressed(KeyboardKey.R)) reversing = !reversing;

		// Move the locomotive
		// TODO: Apply a force (friction based movement)
		trackPosition += reversing ? -speed : speed;
	}

	public override void Draw()
	{
		// Draw the actual locomotive (circle for now)
		Raylib.DrawCircleV(Position, 10f, Color.Magenta);

		// Draw the footplate
		RenderFootplate();
		Utils.DrawRenderTexture(footplate, new Vector2(10, Raylib.GetScreenHeight() - 250 - 10));

		// Say if we're reversing or not and the regulator value
		// TODO: Show visually with levers and whatnot
		Raylib.DrawText($"{regulator.ToString("0.0")}\n\n{reversing}", Raylib.GetScreenWidth() - 150, Raylib.GetScreenHeight() - 70, 40, Color.Black);
	}

	private void RenderFootplate()
	{
		// Begin drawing
		Raylib.BeginTextureMode(footplate);
		Raylib.ClearBackground(Color.Black);

		// Draw the regulator
		float angle = Utils.ConvertRange(regulator, 360, 300);
		Raylib.DrawText($"{angle}", 10, 10, 30, Color.White);

		Utils.DrawAngledLine(new Vector2(125), 120f, angle, 10f, Color.Gray);

		// End drawing
		Raylib.EndTextureMode();
	}

	protected override void CleanUp()
	{
		// Get rid of the footplate
		Raylib.UnloadRenderTexture(footplate);
	}
}