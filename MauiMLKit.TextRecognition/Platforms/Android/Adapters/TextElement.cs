using Com.Google.Mlkit.Vision.Text;
using MLKitSharp.Commons;
using Point = MLKitSharp.Commons.Point;

namespace MLKitSharp.TextRecognition;

public class TextElement(
    string text,
    RectangleF boundingBox,
    List<string> recognizedLanguages,
    List<Point> cornerPoints
)
{
    public string Text { get; private set; } = text;

    public RectangleF BoundingBox { get; private set; } = boundingBox;
    public List<string> RecognizedLanguages { get; private set; } = recognizedLanguages;
    public List<Point> CornerPoints { get; private set; } = cornerPoints;

    public static TextElement FromMLKTextElement(Text.Element mlkTextElement)
    {
        var text = mlkTextElement.Text;
        var boundingBox = RectExtensions.FromRect(mlkTextElement.BoundingBox);
        var recognizedLanguages = new List<string>() { mlkTextElement.RecognizedLanguage };
        var cornerPoints = TextFunctions.ListFromCornerPoints(mlkTextElement.GetCornerPoints());

        return new TextElement(text, boundingBox, recognizedLanguages, cornerPoints);
    }
}
