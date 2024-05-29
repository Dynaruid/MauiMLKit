using Com.Google.Mlkit.Vision.Text;
using MLKitSharp.Commons;
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

    public static TextBlock FromMLKTextBlock(Text.TextBlock mlkTextBlock)
    {
        string text = mlkTextBlock.Text;

        RectangleF boundingBox = RectExtensions.FromRect(mlkTextBlock.BoundingBox);

        var recognizedLanguages = new List<string>() { mlkTextBlock.RecognizedLanguage };

        var cornerPoints = TextFunctions.ListFromCornerPoints(mlkTextBlock.GetCornerPoints());

        var lines = new List<TextLine>();

        foreach (Text.Line line in mlkTextBlock.Lines)
        {
            lines.Add(TextLine.FromMLKTextLine(line));
        }

        return new TextBlock(text, lines, boundingBox, recognizedLanguages, cornerPoints);
    }
}
