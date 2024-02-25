using Raylib_cs;

class Settings
{
	// TODO: Make these editable
	public readonly struct Controls
	{
		// Move left
		public const KeyboardKey Left = KeyboardKey.A;
		public const KeyboardKey LeftAlt = KeyboardKey.Left;

		// Move right
		public const KeyboardKey Right = KeyboardKey.D;
		public const KeyboardKey RightAlt = KeyboardKey.Right;

		// Jump
		public const KeyboardKey Jump = KeyboardKey.Space;
		public const KeyboardKey JumpAlt = KeyboardKey.Null;
    }
}