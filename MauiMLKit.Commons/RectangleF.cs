namespace MLKitSharp.Commons;

public class RectangleF
{
    public int Left { get; private set; }
    public int Top { get; private set; }
    public int Right { get; private set; }
    public int Bottom { get; private set; }

    public int Width => Right - Left;
    public int Height => Bottom - Top;

    public RectangleF(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public static RectangleF FromLTRB(int left, int top, int right, int bottom)
    {
        return new RectangleF(left, top, right, bottom);
    }

    public override string ToString()
    {
        return $"RectangleF(Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}, Width: {Width}, Height: {Height})";
    }
}
