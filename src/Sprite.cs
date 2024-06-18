using System.Numerics;
using Raylib_cs;

class Sprite
{
	public Vector2 Position;
	public float Rotation;

	private Texture2D spritesheet;
	private int width;
	private int height;
	private float perspectiveY = 0.7f;
	private float scale = 5f;

	public Sprite(string path, int singleSpriteWidth, int singleSpriteHeight)
	{
		// Load in the spritesheet
		spritesheet = Raylib.LoadTexture(path);

		// Save the dimensions
		width = singleSpriteWidth;
		height = singleSpriteHeight;
	}

	public virtual void Update()
	{

	}

	public void Render()
	{
		// Loop over every sprite in the spritesheet (stacked on the x)
		Vector2 stackOffset = Vector2.Zero;
		for (int i = 0; i < (spritesheet.Width / width); i++)
		{
			// Select the current sprite from the spritesheet
			Rectangle source = new Rectangle(i * width, 0, width, height);

			// Draw the sprite
			Vector2 size = new Vector2(width, height) * scale;
			Raylib.DrawTexturePro(
				spritesheet,
				source,
				new Rectangle(Position + stackOffset, size),
				size / 2,
				Rotation,
				Color.White
			);

			// Update the stack offset to actually
			// make them look 3d
			stackOffset.Y -= perspectiveY * scale;
		}
	}

	public virtual void CleanUp()
	{
		// Unload the spritesheet
		Raylib.UnloadTexture(spritesheet);
	}
}