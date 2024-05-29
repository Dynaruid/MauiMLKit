using MLKitSharp.Commons;
using MLKitTextRecognitionCommon;
using Point = MLKitSharp.Commons.Point;

namespace MLKitSharp.TextRecognition;

public class TextBlock(
    string text,
    List<TextLine> lines,
    RectangleF boundingBox,
    List<string> recognizedLanguages,
    List<Point> cornerPoints
)
{
    public string Text { get; private set; } = text;
    public List<TextLine> Lines { get; private set; } = lines;
    public RectangleF BoundingBox { get; private set; } = boundingBox;
    public List<string> RecognizedLanguages { get; private set; } = recognizedLanguages;
    public List<Point> CornerPoints { get; private set; } = cornerPoints;

    public static TextBlock FromMLKTextBlock(MLKTextBlock mlkTextBlock)
    {
        string text = mlkTextBlock.Text;

        RectangleF boundingBox = RectExtensions.FromFrame(mlkTextBlock.Frame);

        var recognizedLanguages = TextFunctions.ListFromNSRecognizedLanguages(
            mlkTextBlock.RecognizedLanguages
        );

        var cornerPoints = TextFunctions.ListFromNSCornerPoints(mlkTextBlock.CornerPoints);

        var lines = new List<TextLine>();

        foreach (MLKTextLine line in mlkTextBlock.Lines)
        {
            lines.Add(TextLine.FromMLKTextLine(line));
        }

        return new TextBlock(text, lines, boundingBox, recognizedLanguages, cornerPoints);
    }
}
