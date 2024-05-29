using MLKitSharp.Commons;
using MLKitTextRecognitionCommon;
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

    public static TextElement FromMLKTextElement(MLKTextElement mlkTextElement)
    {
        var text = mlkTextElement.Text;
        var boundingBox = RectExtensions.FromFrame(mlkTextElement.Frame);
        var recognizedLanguages = TextFunctions.ListFromNSRecognizedLanguages(
            mlkTextElement.RecognizedLanguages
        );
        var cornerPoints = TextFunctions.ListFromNSCornerPoints(mlkTextElement.CornerPoints);

        return new TextElement(text, boundingBox, recognizedLanguages, cornerPoints);
    }
}
