using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace MLKitTextRecognitionCommon
{
    [BaseType(typeof(NSObject))]
    interface MLKTextBlock
    {
        // Binding the text property from Objective-C
        [Export("text")]
        string Text { get; }

        // Binding an NSArray to a List<T> for easier use in C#
        [Export("lines")]
        MLKTextLine[] Lines { get; }

        // Binding the CGRect structure
        [Export("frame")]
        CGRect Frame { get; } //rect,boundingBox

        // Binding languages array similarly
        [Export("recognizedLanguages")]
        MLKTextRecognizedLanguage[] RecognizedLanguages { get; }

        // Binding the corner points array, translating NSValue to CGPoint in a List<CGPoint>
        [Export("cornerPoints")]
        NSValue[] CornerPoints { get; } //points

        // Marking the default constructor as unavailable
        [Unavailable(PlatformName.iOS, "Default constructor is not available.")]
        MLKTextBlock Constructor();
    }
}
