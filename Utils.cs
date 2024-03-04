using System.Numerics;

class Utils
{
    // Get the distance between two things
    public static float Distance(Vector2 pointOne, Vector2 pointTwo)
    {
        // TODO: Don't use square root
        float distance = MathF.Pow(pointTwo.X - pointOne.X, 2) + MathF.Pow(pointTwo.Y - pointOne.Y, 2);
        return MathF.Sqrt(distance);
    }
}