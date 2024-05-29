using Foundation;
using MLKitTextRecognition;
using MLKitTextRecognitionChinese;
using MLKitTextRecognitionCommon;
using MLKitTextRecognitionDevanagari;
using MLKitTextRecognitionJapanese;
using MLKitTextRecognitionKorean;
using MLKitVision;

namespace MLKitSharp.TextRecognition;

public class TextRecognitionNative
{
    public Action<RecognizedText>? CallbackAction { get; set; }
    private Dictionary<string, MLKTextRecognizer> TextRecognizerInstances = [];

    private static TextRecognitionNative? Instance { get; set; }
    public static TextRecognitionNative SharedInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new TextRecognitionNative();
            }
            return Instance;
        }
    }

    private static MLKTextRecognizer TextRecognizerInitialize(int scriptValue)
    {
        MLKCommonTextRecognizerOptions options;
        switch (scriptValue)
        {
            default:
                options = new MLKTextRecognizerOptions();
                break;
            case 1:
                options = new MLKChineseTextRecognizerOptions();
                break;
            case 2:
                options = new MLKDevanagariTextRecognizerOptions();
                break;
            case 3:
                options = new MLKJapaneseTextRecognizerOptions();
                break;
            case 4:
                options = new MLKKoreanTextRecognizerOptions();
                break;
        }

        return MLKTextRecognizer.TextRecognizerWithOptions(options);
    }

    public void CloseTextRecognizerWithUid(string uid)
    {
        TextRecognizerInstances.Remove(uid);
    }

    public void StartTextRecognizerWithInputs(string uid, int scriptValue, MLKVisionImage image)
    {
        if (CallbackAction == null)
        {
            return;
        }

        MLKTextRecognizer textRecognizer;
        if (TextRecognizerInstances.ContainsKey(uid) == false)
        {
            textRecognizer = TextRecognizerInitialize(scriptValue);
            TextRecognizerInstances[uid] = textRecognizer;
        }
        else
        {
            textRecognizer = TextRecognizerInstances[uid];
        }
        textRecognizer.ProcessImage(
            image,
            (MLKText? visionText, NSError? error) =>
            {
                if (error != null)
                {
                    CallbackAction(RecognizedText.FromError1());
                    return;
                }
                else if (visionText == null)
                {
                    CallbackAction.Invoke(RecognizedText.FromEmpty());
                    return;
                }

                RecognizedText recognizedText = RecognizedText.FromMLKText(visionText!);

                CallbackAction(recognizedText);
            }
        );
    }
}
