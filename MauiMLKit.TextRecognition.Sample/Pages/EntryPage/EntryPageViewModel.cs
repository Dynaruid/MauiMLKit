using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MLKitSharp.Commons;
using MLKitSharp.TextRecognition;
using SkiaSharp;
using Size = MLKitSharp.Commons.Size;

namespace MLKitSharp.TextRecognition.Sample.Pages.EntryPage;

public partial class EntryPageViewModel : ObservableObject
{
    [ObservableProperty]
    string imageText = string.Empty;

    [ObservableProperty]
    private ImageSource currentImageSource = ImageSource.FromFile("dotnet_bot.png");

    [ObservableProperty]
    private bool isImageLoaded = false;

    [ObservableProperty]
    private string selectedRecognitionLanguages = "English";
    public OrderedDictionary RecognitionLanguages =
        new()
        {
            { "English", TextRecognitionScript.Latin },
            { "Chinese", TextRecognitionScript.Chinese },
            { "Devanagari", TextRecognitionScript.Devanagiri },
            { "Japanese", TextRecognitionScript.Japanese },
            { "Korean", TextRecognitionScript.Korean },
        };

    private TextRecognitionScript CurrentTextRecognizerScript = TextRecognitionScript.Latin;

    public AsyncRelayCommand LoadImageCommand { get; }
    private TextRecognizer? TextRecognizer { get; set; }

    private SKBitmap? CurrentBitmapImg;

    public EntryPageViewModel()
    {
        LoadImageCommand = new AsyncRelayCommand(PickAndShowImage);
    }

    partial void OnSelectedRecognitionLanguagesChanging(string value)
    {
        if (TextRecognizer != null)
        {
            TextRecognizer.Close();
            TextRecognizer = null;
        }

        CurrentTextRecognizerScript = (TextRecognitionScript)RecognitionLanguages[value]!;
    }

    private async Task<string?> PickAndShowImage()
    {
        try
        {
            FileResult? result;
            if (await RequestStoragePermissionAsync())
            {
                result = await MediaPicker.Default.PickPhotoAsync();

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var managedStream = new SKManagedStream(stream);
                    var skBitmap = SKBitmap.Decode(managedStream);

                    if (skBitmap != null)
                    {
                        CurrentBitmapImg = skBitmap;

                        var width = skBitmap.Width;
                        var height = skBitmap.Height;
                        var channels = skBitmap.BytesPerPixel;

                        Console.WriteLine(
                            "width: "
                                + width.ToString()
                                + " height: "
                                + height.ToString()
                                + " channels: "
                                + channels.ToString()
                        );
                        if (TextRecognizer == null)
                        {
                            TextRecognizer = new TextRecognizer(
                                result =>
                                {
                                    Console.WriteLine(result.Text);
                                    ImageText = result.Text;
                                    IsImageLoaded = true;
                                },
                                CurrentTextRecognizerScript
                            );
                        }

                        var inputImageMetadata = new SourceImageMetadata(
                            new Size(width, height),
                            SourceImageRotation.Rotation0deg,
                            SourceImageFormat.Bgra8888,
                            skBitmap.RowBytes
                        );
                        IntPtr buffer = skBitmap.GetPixels();

                        byte[] pixels = new byte[skBitmap.ByteCount];
                        System.Runtime.InteropServices.Marshal.Copy(
                            buffer,
                            pixels,
                            0,
                            skBitmap.ByteCount
                        );
#if ANDROID
                        SourceImage inputImage = SourceImage.FromFilePath(result.FullPath);
#elif IOS
                        SourceImage inputImage = SourceImage.FromBytes(pixels, inputImageMetadata);
#else
                        Console.WriteLine("not support os!!");
                        return;
#endif
                        TextRecognizer.ProcessImage(inputImage);
                        SkBitmapSetImageSource();
                    }

                    return result.FullPath;
                }
                else
                {
                    IsImageLoaded = false;
                }
            }
            else
            {
                // 권한 거부에 대한 처리
                Console.WriteLine("갤러리 접근 권한이 거부되었습니다.");
                IsImageLoaded = false;
                return "";
            }
        }
        catch (Exception ex)
        {
            // 에러 처리
            Console.WriteLine($"Error picking image: {ex.Message}");
        }

        return null;
    }

    public void ChangeTextRecognizerScript() { }

    public void SkBitmapSetImageSource()
    {
        using var data = CurrentBitmapImg!.Encode(SKEncodedImageFormat.Png, 100);
        var stream = new MemoryStream();
        data.SaveTo(stream);
        stream.Seek(0, SeekOrigin.Begin);
        CurrentImageSource = ImageSource.FromStream(() =>
        {
            return stream;
        });
        CurrentBitmapImg.Dispose();
        CurrentBitmapImg = null;
    }

    public async Task<bool> RequestStoragePermissionAsync()
    {
        // 권한 상태를 확인합니다.
        var status = await Permissions.CheckStatusAsync<Permissions.Photos>();

        if (status != PermissionStatus.Granted)
        {
            // 권한이 허용되지 않았다면 사용자에게 권한 요청
            status = await Permissions.RequestAsync<Permissions.Photos>();
        }

        // 권한 허용 여부 반환
        return status == PermissionStatus.Granted;
    }
}
