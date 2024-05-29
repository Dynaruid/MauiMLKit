using System;
using Foundation;
using MLKitTextRecognitionCommon;

namespace MLKitTextRecognitionKorean
{
    [BaseType(typeof(MLKCommonTextRecognizerOptions))]
    interface MLKKoreanTextRecognizerOptions
    {
        // Initializes a TextRecognizerOptions instance with the default values.
        [Static]
        [Export("init")]
        MLKKoreanTextRecognizerOptions init();
    }
}
