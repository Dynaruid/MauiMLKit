using MLKitSharp.Commons;
using Rect = Android.Graphics.Rect;

namespace MLKitSharp.TextRecognition;

public static class RectExtensions
{
    public static RectangleF FromRect(Rect? rect)
    {
        if (rect == null)
        {
            return RectangleF.FromLTRB(0, 0, 0, 0);
        }
        return RectangleF.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
    }
}
