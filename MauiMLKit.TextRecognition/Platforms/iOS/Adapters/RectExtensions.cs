using CoreGraphics;
using MLKitSharp.Commons;

namespace MLKitSharp.TextRecognition;

public static class RectExtensions
{
    public static RectangleF FromFrame(CGRect frame)
    {
        return RectangleF.FromLTRB(
            (int)frame.Left.Value,
            (int)frame.Top.Value,
            (int)frame.Right.Value,
            (int)frame.Bottom.Value
        );
    }
}
