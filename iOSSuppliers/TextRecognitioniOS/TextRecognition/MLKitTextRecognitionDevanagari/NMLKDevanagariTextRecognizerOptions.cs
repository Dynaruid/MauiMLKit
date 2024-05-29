using System;
using Foundation;
using MLKitTextRecognitionCommon;

namespace MLKitTextRecognitionDevanagari
{
    // Defines configurations for a text recognizer for Latin-based languages.
    [BaseType(typeof(MLKCommonTextRecognizerOptions))]
    interface MLKDevanagariTextRecognizerOptions
    {
        // Initializes a TextRecognizerOptions instance with the default values.
        [Static]
        [Export("init")]
        MLKDevanagariTextRecognizerOptions init();
    }
}
