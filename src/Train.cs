// using System.Numerics;
// using Raylib_cs;

// class Train
// {
// 	public Vector2 Position;

// 	private List<Bezier> track;
// 	public float PositionOnCurrentTrack;
// 	public int CurrentTrackIndex;


// 	public Train(List<Bezier> railway)
// 	{
// 		// Assign the track
// 		track = railway;

// 		// Set the start position to be at the
// 		// start of the first bit of track
// 		PositionOnCurrentTrack = 0f;
// 		Position = track[0].GetPositionFromPoint(PositionOnCurrentTrack);
// 	}

// 	public void Update()
// 	{
// 		// Check for if we wanna move the train
// 		// TODO: Rewrite to act like a train and not player
// 		if (Raylib.IsKeyDown(KeyboardKey.Right)) PositionOnCurrentTrack += 0.01f;
// 		if (Raylib.IsKeyDown(KeyboardKey.Left)) PositionOnCurrentTrack -= 0.01f;

// 		// Check for if we need to move
// 		// onto the next bit of track
// 		if (PositionOnCurrentTrack > 1)
// 		{
// 			// The -1 makes sure that we still keep any overlap
// 			// because we're checking for if its greater
// 			//? idk if this actually does anything
// 			PositionOnCurrentTrack = 1 - PositionOnCurrentTrack;
// 			CurrentTrackIndex++;
// 		}
// 		else if (PositionOnCurrentTrack < 0)
// 		{
// 			// The -1 makes sure that we still keep any overlap
// 			// because we're checking for if its greater
// 			//? idk if this actually does anything
// 			PositionOnCurrentTrack = 1 - PositionOnCurrentTrack;
// 			CurrentTrackIndex--;
// 		}
		
// 		// Update the position based on the track
// 		Position = track[CurrentTrackIndex].GetPositionFromPoint(PositionOnCurrentTrack);
// 	}

// 	public void Render()
// 	{
// 		// Circle for now
// 		Raylib.DrawCircleV(Position, 10f, Color.Blue);
// 	}
// }