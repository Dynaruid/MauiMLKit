using System;
using Foundation;

namespace MLKitVision
{
    // Using the [Protocol] attribute to indicate that this C# interface corresponds to an Objective-C protocol.
    // The interface inherits from INativeObject and IDisposable to properly manage memory and object lifecycle.
    [Protocol(Name = "MLKCompatibleImage"), Model]
    [BaseType(typeof(NSObject))]
    interface MLKCompatibleImage
    {
        // Since the protocol does not define any methods or properties,
        // and is not intended to be implemented by users (as per the Objective-C header comment),
        // we do not include any members here.

        // This interface acts primarily as a marker interface to ensure type safety
        // and compatibility with ML Kit's expectations in other parts of your application.
    }
}
