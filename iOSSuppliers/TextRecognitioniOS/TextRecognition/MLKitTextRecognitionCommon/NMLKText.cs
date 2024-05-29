using System;
using Foundation;
using ObjCRuntime;

namespace MLKitTextRecognitionCommon
{
    // Define the MLKTextBlock class or ensure it is defined elsewhere in your bindings
    // Assuming MLKTextBlock is already defined, or will be defined in your project.

    [BaseType(typeof(NSObject))]
    // Swift name mapping if needed in Swift projects
    interface MLKText
    {
        // Using Export to bind the Objective-C properties to C# properties
        [Export("text")]
        string Text { get; }

        // Use NSArray directly, avoiding generic List<T>
        [Export("blocks")]
        MLKTextBlock[] Blocks { get; }

        // Indicate that the default initializer is not available
        [Unavailable(PlatformName.iOS, "Default constructor is not available.")]
        MLKText Constructor();
    }
}
