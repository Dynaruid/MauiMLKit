using AndroidPoint = Android.Graphics.Point;
using Point = MLKitSharp.Commons.Point;

namespace MLKitSharp.TextRecognition;

public static class TextFunctions
{
    public static List<Point> ListFromCornerPoints(AndroidPoint[]? points)
    {
        var cornerPoints = new List<Point>();
        if (points != null)
        {
            foreach (AndroidPoint point in points)
            {
                int x = point.X;
                int y = point.Y;
                cornerPoints.Add(new Point(x, y));
            }
        }
        return cornerPoints;
    }
}
