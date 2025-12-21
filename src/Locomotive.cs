using System.Numerics;
using Raylib_cs;

class Bogey
{
	//? 0f = start
	//? 1f = end
	public float PositionOnTrack = 0;
	public Track Track;

	//? movement is how much to move, in 'units', on the track (negative is backwards)
	public void Move(float movement)
	{
		// Convert the movement into the percentage
		float movementDelta = Track.Length - movement;
		float percentage = movementDelta / Track.Length;

		// Update our position
		PositionOnTrack += percentage;

		// Check for if we need to move onto the next track
		if (PositionOnTrack > 1f)
		{
			if (Track.Next != null)
			{
				PositionOnTrack -= 1f;
				Track = Track.Next;
			}
			else PositionOnTrack = 1f;
		}

		// Check for if we need to move onto the previous track
		if (PositionOnTrack < 0f)
		{
			if (Track.Previous != null)
			{
				PositionOnTrack -= 1f;
				Track = Track.Previous;
			}
			else PositionOnTrack = 0f;
		}

	}
}

class Locomotive
{
	private float speed = 100f;

	private Bogey bogey;
	// private Bogey frontBogey;
	// private Bogey backBogey;

	private Texture2D texture;

	public Locomotive(Track spawnTrack, float spawnPositionPercentage)
	{
		bogey = new Bogey();

		bogey.Track = spawnTrack;
		bogey.PositionOnTrack = spawnPositionPercentage;

		texture = Raylib.LoadTexture("./assets/locomotive.png");
	}

	public void Update()
	{
		float direction = 0;
		if (Raylib.IsKeyDown(KeyboardKey.Left)) direction--;
		if (Raylib.IsKeyDown(KeyboardKey.Right)) direction++;

		float movement = direction * speed;
		bogey.Move(movement);
	}

	public void Draw()
	{
		// TODO: Draw the thing between the two bogeys. like a real train
		Raylib.DrawTextureEx(texture, bogey.Track.Position, 0f, 0.1f, Color.White);

		Raylib.DrawText($"{bogey.PositionOnTrack}", 10, 10, 30, Color.White);
	}

	public void Unload()
	{
		Raylib.UnloadTexture(texture);
	}
}