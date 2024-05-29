using System;
using Foundation;
using ObjCRuntime;

namespace MLKitTextRecognitionCommon
{
    // Define the MLKTextRecognizedLanguage class
    [BaseType(typeof(NSObject))]
    interface MLKTextRecognizedLanguage
    {
        // Bind the Objective-C property to a C# property
        [Export("languageCode")]
        [NullAllowed] // Allows the language code to be null if not available
        string LanguageCode { get; }

        // Marking the default constructor as unavailable
        [Unavailable(PlatformName.iOS, "Default constructor is not available.")]
        MLKTextRecognizedLanguage Constructor();
    }
}
