using System;
using Foundation;
using MLKitTextRecognitionCommon;

namespace MLKitTextRecognition
{
    // Defines configurations for a text recognizer for Latin-based languages.
    [BaseType(typeof(MLKCommonTextRecognizerOptions))]
    interface MLKTextRecognizerOptions
    {
        // Initializes a TextRecognizerOptions instance with the default values.
        [Static]
        [Export("init")]
        MLKTextRecognizerOptions init();
    }
}
