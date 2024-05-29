using Com.Google.Mlkit.Vision.Text;
using Java.Lang;
using MLKitSharp.Commons;
using TextRecognitionNative = Com.Mlkittextrecognitionadapter.MLTextRecognition;

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
        NativeTextRecognizer = TextRecognitionNative.SharedInstance!;
        NativeTextRecognizer.SetCallbackAction(new ResultConsumer(onResult));

        Script = script;
        Id = DateTime.Now.Ticks.ToString(); // Using ticks as a unique identifier
    }

    public void ProcessImage(SourceImage sourceImage)
    {
        NativeTextRecognizer.StartTextRecognizerWithInputs(
            Id,
            (int)Script,
            sourceImage.VisionImageFromData()
        );
    }

    public void Close()
    {
        NativeTextRecognizer.CloseTextRecognizerWithUid(Id);
    }
}

public class ResultConsumer(Action<RecognizedText> resultAction)
    : Java.Lang.Object,
        Java.Util.Functions.IBiConsumer
{
    public void Accept(Java.Lang.Object? resultTextObj, Java.Lang.Object? errerObj)
    {
        if (errerObj != null)
        {
            Integer errer = (Integer)errerObj;
            if (errer.IntValue() != 0)
            {
                resultAction(RecognizedText.FromError1());
            }
            else
            {
                if (resultTextObj == null)
                {
                    resultAction(RecognizedText.FromEmpty());
                }
                else
                {
                    resultAction(RecognizedText.FromMLKText((Text)resultTextObj));
                }
            }
        }
    }
}
