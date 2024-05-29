using MLKitSharp.Commons;
using MLKitTextRecognitionCommon;
using Point = MLKitSharp.Commons.Point;

namespace MLKitSharp.TextRecognition;

public class TextLine(
    string text,
    List<TextElement> elements,
    RectangleF boundingBox,
    List<string> recognizedLanguages,
    List<Point> cornerPoints,
    double? confidence,
    double? angle
)
{
    public string Text { get; private set; } = text;
    public List<TextElement> Elements { get; private set; } = elements;
    public RectangleF BoundingBox { get; private set; } = boundingBox;
    public List<string> RecognizedLanguages { get; private set; } = recognizedLanguages;
    public List<Point> CornerPoints { get; private set; } = cornerPoints;
    public double? Confidence { get; private set; } = confidence;
    public double? Angle { get; private set; } = angle;

    public static TextLine FromMLKTextLine(MLKTextLine mlkTextLine)
    {
        var text = mlkTextLine.Text;
        var boundingBox = RectExtensions.FromFrame(mlkTextLine.Frame);
        var recognizedLanguages = TextFunctions.ListFromNSRecognizedLanguages(
            mlkTextLine.RecognizedLanguages
        );
        var cornerPoints = TextFunctions.ListFromNSCornerPoints(mlkTextLine.CornerPoints);

        var elements = new List<TextElement>();

        foreach (MLKTextElement element in mlkTextLine.Elements)
        {
            elements.Add(TextElement.FromMLKTextElement(element));
        }

        return new TextLine(
            text,
            elements,
            boundingBox,
            recognizedLanguages,
            cornerPoints,
            null,
            null
        );
    }
}
