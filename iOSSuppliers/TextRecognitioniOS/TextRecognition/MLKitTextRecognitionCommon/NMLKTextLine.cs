using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace MLKitTextRecognitionCommon
{
    // Assume that MLKTextElement and MLKTextRecognizedLanguage are defined elsewhere in your bindings.

    [BaseType(typeof(NSObject))]
    interface MLKTextLine
    {
        // Binding the text property from Objective-C
        [Export("text")]
        string Text { get; }

        // Binding the array of text elements to a List<T> for easier use in C#
        [Export("elements")]
        MLKTextElement[] Elements { get; }

        // Binding the CGRect for the frame of the text line
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
        MLKTextLine Constructor();
    }
}
