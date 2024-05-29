using System;
using Foundation;
using MLKitTextRecognitionCommon;

namespace MLKitTextRecognitionJapanese
{
    // Defines configurations for a text recognizer for Latin-based languages.
    [BaseType(typeof(MLKCommonTextRecognizerOptions))]
    interface MLKJapaneseTextRecognizerOptions
    {
        // Initializes a TextRecognizerOptions instance with the default values.
        [Static]
        [Export("init")]
        MLKJapaneseTextRecognizerOptions init();
    }
}
