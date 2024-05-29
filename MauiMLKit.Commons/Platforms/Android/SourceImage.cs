using Com.Google.Mlkit.Vision.Common;

namespace MLKitSharp.Commons;

#pragma warning disable CS0436
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

    public InputImage VisionImageFromData()
    {
        InputImage? inputImage;
        if (ImageType == SourceImageType.file)
        {
            inputImage = InputImage.FromFilePath(
                Platform.AppContext,
                Android.Net.Uri.FromFile(new Java.IO.File(FilePath!))!
            );
            return inputImage;
        }
        else if (ImageType == SourceImageType.bytes)
        {
            inputImage = InputImage.FromByteArray(
                ImageBytes!,
                Metadata!.Size.Width,
                Metadata!.Size.Height,
                (int)Metadata!.Rotation,
                Metadata!.BytesPerRow
            );
            return inputImage;
        }
        else
        {
            throw new ArgumentException($"No image type ");
        }
    }
}
#pragma warning restore CS0436
