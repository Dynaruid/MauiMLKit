using CoreGraphics;
using CoreImage;
using CoreVideo;
using MLKitVision;
using UIKit;

namespace MLKitSharp.Commons;

public class SourceImage
{
    public string? FilePath { get; private set; }
    public byte[]? ImageBytes { get; private set; }
    public SourceImageType ImageType { get; private set; }
    public SourceImageMetadata? Metadata { get; private set; }

    private SourceImage(
        string? filePath,
        byte[]? imageBytes,
        SourceImageType imageType,
        SourceImageMetadata? metadata
    )
    {
        FilePath = filePath;
        ImageBytes = imageBytes;
        ImageType = imageType;
        Metadata = metadata;
    }

    public static SourceImage FromFilePath(string path)
    {
        return new SourceImage(path, null, SourceImageType.file, null);
    }

    public static SourceImage FromFile(FileInfo file)
    {
        return new SourceImage(file.FullName, null, SourceImageType.file, null);
    }

    public static SourceImage FromBytes(byte[] bytes, SourceImageMetadata metadata)
    {
        return new SourceImage(null, bytes, SourceImageType.bytes, metadata);
    }

    public MLKVisionImage VisionImageFromData()
    {
        if (ImageType == SourceImageType.file)
        {
            return FilePathToVisionImage();
        }
        else if (ImageType == SourceImageType.bytes)
        {
            return BytesToVisionImage();
        }
        else
        {
            throw new ArgumentException($"No image type ");
        }
    }

    private MLKVisionImage FilePathToVisionImage()
    {
        UIImage image = new UIImage(FilePath!);
        MLKVisionImage visionImage = new MLKVisionImage().InitWithImage(image);
        visionImage.Orientation = image.Orientation;
        return visionImage;
    }

    private MLKVisionImage BytesToVisionImage()
    {
        SourceImageMetadata metadata = Metadata!;
#pragma warning disable CA1416
        using CVPixelBuffer? pxBuffer = CVPixelBuffer.Create(
            metadata.Size.Width,
            metadata.Size.Height,
            (CVPixelFormatType)metadata.Format,
            ImageBytes!,
            metadata.BytesPerRow,
            new CVPixelBufferAttributes()
        );
#pragma warning restore CA1416
        return PixelBufferToVisionImage(pxBuffer!);
    }

    private static MLKVisionImage PixelBufferToVisionImage(CVPixelBuffer pixelBuffer)
    {
        CIImage ciImage = new CIImage(pixelBuffer);

        CIContext temporaryContext = new CIContext();

        using CGImage videoImage = temporaryContext.CreateCGImage(
            ciImage,
            new CGRect(0, 0, pixelBuffer.Width, pixelBuffer.Height)
        )!;

        UIImage uiImage = new UIImage(videoImage);

        var mlkVisionImage = new MLKVisionImage().InitWithImage(uiImage);

        return mlkVisionImage;
    }
}
