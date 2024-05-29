namespace MLKitSharp.Commons;

public class Point(int x, int y)
{
    public int X { get; private set; } = x;
    public int Y { get; private set; } = y;
}
