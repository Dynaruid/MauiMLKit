using System;
using CoreMedia;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace MLKitVision
{
    [BaseType(typeof(MLKCompatibleImage))]
    interface MLKVisionImage
    {
        [Export("orientation", ArgumentSemantic.Assign)]
        UIImageOrientation Orientation { get; set; }

        [Export("initWithImage:")]
        MLKVisionImage InitWithImage(UIImage image);

        [Export("initWithBuffer:")]
        MLKVisionImage InitWithBuffer(CMSampleBuffer sampleBuffer);

        [Unavailable(PlatformName.iOS, "Default constructor is not available.")]
        MLKVisionImage Constructor();
    }
}
