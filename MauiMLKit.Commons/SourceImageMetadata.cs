namespace MLKitSharp.Commons;

public class SourceImageMetadata
{
    public Size Size { get; }
    public SourceImageRotation Rotation { get; }
    public SourceImageFormat Format { get; }
    public int BytesPerRow { get; }

    public SourceImageMetadata(
        Size size,
        SourceImageRotation rotation,
        SourceImageFormat format,
        int bytesPerRow
    )
    {
        Size = size;
        Rotation = rotation;
        Format = format;
        BytesPerRow = bytesPerRow;
    }
}
