using System;
using Foundation;
using ObjCRuntime;

namespace MLKitTextRecognitionCommon
{
    // Define the MLKCommonTextRecognizerOptions class as a base class for further specialization
    [BaseType(typeof(NSObject))]
    interface MLKCommonTextRecognizerOptions
    {
        // Marking the default constructor as unavailable
        [Unavailable(PlatformName.iOS, "Default constructor is not available.")]
        MLKCommonTextRecognizerOptions Constructor();
    }
}
