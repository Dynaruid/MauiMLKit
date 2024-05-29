using System;
using Foundation;
using MLKitTextRecognitionCommon;

namespace MLKitTextRecognitionChinese
{
    // Defines configurations for a text recognizer for Latin-based languages.
    [BaseType(typeof(MLKCommonTextRecognizerOptions))]
    interface MLKChineseTextRecognizerOptions
    {
        // Initializes a TextRecognizerOptions instance with the default values.
        [Static]
        [Export("init")]
        MLKChineseTextRecognizerOptions init();
    }
}
