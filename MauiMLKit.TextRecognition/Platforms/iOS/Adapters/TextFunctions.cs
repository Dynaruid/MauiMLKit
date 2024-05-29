using Foundation;
using MLKitTextRecognitionCommon;
using Point = MLKitSharp.Commons.Point;

namespace MLKitSharp.TextRecognition;

public static class TextFunctions
{
    public static List<string> ListFormRecognizedLanguages(List<object> languages)
    {
        var recognizedLanguages = new List<string>();
        foreach (var language in languages)
        {
            if (language != null)
            {
                recognizedLanguages.Add(language.ToString()!);
            }
        }
        return recognizedLanguages;
    }

    public static List<string> ListFromNSRecognizedLanguages(MLKTextRecognizedLanguage[] languages)
    {
        var recognizedLanguages = new List<string>();
        foreach (MLKTextRecognizedLanguage language in languages)
        {
            if (language != null)
            {
                recognizedLanguages.Add(language.LanguageCode!);
            }
        }
        return recognizedLanguages;
    }

    public static List<Point> ListFromCornerPoints(List<Dictionary<string, object>> points)
    {
        var cornerPoints = new List<Point>();
        foreach (var point in points)
        {
            int x = Convert.ToInt32(point["x"]);
            int y = Convert.ToInt32(point["y"]);
            cornerPoints.Add(new Point(x, y));
        }
        return cornerPoints;
    }

    public static List<Point> ListFromNSCornerPoints(NSValue[] points)
    {
        var cornerPoints = new List<Point>();
        foreach (NSValue point in points)
        {
            int x = Convert.ToInt32(point.CGPointValue.X);
            int y = Convert.ToInt32(point.CGPointValue.Y);
            cornerPoints.Add(new Point(x, y));
        }

        return cornerPoints;
    }
}
