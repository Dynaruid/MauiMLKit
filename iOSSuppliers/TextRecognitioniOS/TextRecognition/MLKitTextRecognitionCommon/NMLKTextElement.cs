using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace MLKitTextRecognitionCommon
{
    // Assuming MLKTextRecognizedLanguage is defined elsewhere in your bindings.

    [BaseType(typeof(NSObject))]
    interface MLKTextElement
    {
        // Binding the text property from Objective-C
        [Export("text")]
        string Text { get; }

        // Binding the CGRect for the frame of the text element
        [Export("frame")]
        CGRect Frame { get; }

        // Binding the array of recognized languages to a List<T>
        [Export("recognizedLanguages")]
        MLKTextRecognizedLanguage[] RecognizedLanguages { get; }

        // Binding the corner points, converting NSValue to CGPoint
        [Export("cornerPoints")]
        NSValue[] CornerPoints { get; }

        // Marking the default constructor as unavailable
        [Unavailable(PlatformName.iOS, "Default constructor is not available.")]
        MLKTextElement Constructor();
    }
}
