using MLKitSharp.Commons;
using MLKitVision;

namespace MLKitSharp.TextRecognition;

public class TextRecognizer
{
    private TextRecognitionNative NativeTextRecognizer { get; set; }

    public TextRecognitionScript Script { get; private set; }
    public string Id { get; private set; }

    public TextRecognizer(
        Action<RecognizedText> onResult,
        TextRecognitionScript script = TextRecognitionScript.Latin
    )
    {
        NativeTextRecognizer = TextRecognitionNative.SharedInstance;
        NativeTextRecognizer.CallbackAction = onResult;

        Script = script;
        Id = DateTime.Now.Ticks.ToString(); // Using ticks as a unique identifier
    }

    public void ProcessImage(SourceImage sourceImage)
    {
        MLKVisionImage image = sourceImage.VisionImageFromData();
        NativeTextRecognizer.StartTextRecognizerWithInputs(Id, (int)Script, image);
    }

    public void Close()
    {
        NativeTextRecognizer.CloseTextRecognizerWithUid(Id);
    }
}
