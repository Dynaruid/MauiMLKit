# Google's ML Kit for MAUI

[구글 standalone ML Kit](https://developers.google.com/ml-kit)를 MAUI에서 사용할수있게 만든 라이브러리 입니다.


## Supported Modules

현재는 Text Recognition 모듈만 바인딩한 상태입니다.

| Module                                     | Android | iOS |
| ------------------------------------------ | :-----: | :-: |
| [Image Labeling](./image-labeling)         |   ❌    | ❌  |
| [Identify Languages](./identify-languages) |   ❌    | ❌  |
| [Face Detection](./face-detection)         |   ❌    | ❌  |
| [Text Recognition](./text-recognition)     |   ✅    | ✅  |
| [Barcode Scanning](./barcode-scanning)     |   ❌    | ❌  |
| [Translate Text](./translate-text)         |   ❌    | ❌  |
| Object Detection and Tracking              |   ❌    | ❌  |
| Digital Ink Recognition                    |   ❌    | ❌  |
| Smart Replies                              |   ❌    | ❌  |

## 필요 조건 (Prerequisites)

MAUI앱 빌드가 가능한 환경이어야 합니다.

iOS 16.0 이상, Android api 30 이상이어야 합니다.

iOS를 타겟으로 하는경우 XCode의 Command Line Tools, CocoaPods이 설치되어 있어야 합니다.

iOS에서 카메라 또는 이미지 파일 사용을 위한 권한이 필요합니다. 
Info.plist에 아래 블럭을 추가하세요.
```
<key>NSCameraUsageDescription</key>
<string>This app needs access to the camera to take photos.</string>
<key>NSMicrophoneUsageDescription</key>
<string>This app needs access to microphone for taking videos.</string>
<key>NSPhotoLibraryAddUsageDescription</key>
<string>This app needs access to the photo gallery for picking photos and videos.</string>
<key>NSPhotoLibraryUsageDescription</key>
<string>This app needs access to photos gallery for picking photos and videos.</string>
```

## 사용법 (Usage)

iOS시뮬레이터를 타겟으로 하는경우 앱이 arm64가 아닌 x64를 대상으로 빌드되어야합니다. 애플실리콘 맥에서도 iOS시뮬레이터를 위해 x64앱을 빌드할수있습니다.

아래 코드처럼 세팅이 가능합니다.

csproj에서 MauiMLKit프로젝트를 로드할때 bool값을 가지는 IOS_SIMULATOR 프로퍼티를 전달해야합니다.
```
  <PropertyGroup>
    <RuntimeIdentifier Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'ios'"
    >iossimulator-x64</RuntimeIdentifier>
  </PropertyGroup>

  <PropertyGroup Condition="$(RuntimeIdentifier.Contains('iossimulator')) == 'True'">
    <IOS_SIMULATOR>True</IOS_SIMULATOR>
  </PropertyGroup>
  <PropertyGroup Condition="$(RuntimeIdentifier.Contains('iossimulator')) == 'False'">
    <IOS_SIMULATOR>False</IOS_SIMULATOR>
  </PropertyGroup>

  <PropertyGroup>
    <HasChinese>True</HasChinese>
    <HasDevanagari>True</HasDevanagari>
    <HasJapanese>True</HasJapanese>
    <HasKorean>True</HasKorean>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Condition="'$(HasChinese)' == 'True'"
      Include="../MauiMLKit.TextRecognitionChinese/MauiMLKit.TextRecognitionChinese.csproj" Properties="IOS_SIMULATOR=$(IOS_SIMULATOR)" />
    <ProjectReference Condition="'$(HasDevanagari)' == 'True'"
      Include="../MauiMLKit.TextRecognitionDevanagari/MauiMLKit.TextRecognitionDevanagari.csproj" Properties="IOS_SIMULATOR=$(IOS_SIMULATOR)" />
    <ProjectReference Condition="'$(HasJapanese)' == 'True'"
      Include="../MauiMLKit.TextRecognitionJapanese/MauiMLKit.TextRecognitionJapanese.csproj" Properties="IOS_SIMULATOR=$(IOS_SIMULATOR)" />
    <ProjectReference Condition="'$(HasKorean)' == 'True'"
      Include="../MauiMLKit.TextRecognitionKorean/MauiMLKit.TextRecognitionKorean.csproj" Properties="IOS_SIMULATOR=$(IOS_SIMULATOR)" />

    <ProjectReference
      Include="../MauiMLKit.Commons/MauiMLKit.Commons.csproj"
      Properties="IOS_SIMULATOR=$(IOS_SIMULATOR)"
    />
    <ProjectReference
      Include="../MauiMLKit.TextRecognition/MauiMLKit.TextRecognition.csproj"
      Properties="IOS_SIMULATOR=$(IOS_SIMULATOR)"
    />
  </ItemGroup>
```
위 코드는 안드로이드 빌드에 영향을 미치지 않으므로 그대로 사용하셔도 됩니다.

TextRecognition의 사용법은 MauiMLKit.TextRecognition.Sample 프로젝트의 EntryPageViewModel.cs를 참고 하세요.
```
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
                        // 안드로이드에서 SourceImage.FromBytes로 SourceImage를 만들기 위해서는 이미지 바이트배열이 NV21이어야 합니다.
                        SourceImage inputImage = SourceImage.FromFilePath(result.FullPath);

#elif IOS
                        // IOS에서 SourceImage.FromBytes로 SourceImage를 만들기 위해서는 이미지 바이트배열이 BGRA8888이어야 합니다.
                        // 글자인식에 있어서는 RGBA배치또한 정상적으로 인식 됩니다.
                        SourceImage inputImage = SourceImage.FromBytes(pixels, inputImageMetadata);
#else
                        Console.WriteLine("not support os!!");
                        return;
#endif
                        TextRecognizer.ProcessImage(inputImage);
                        
                    }
```

## 해결 중인 문제점 (Known Issues) 및 주의사항 (Caveats)

이 프로젝트를 안드로이드 대상으로 처음 빌드할경우 빌드한 결과물을 디바이스또는 시뮬레이터에서 실행할경우 작동되지 않는 문제가 있습니다.
이 프로젝트의 MSBuild 동작에는 안드로이드 라이브러리(aar, jar파일)를 Maven저장소에서 다운로드 받는 과정이 있습니다. 
문제는 이 과정이 msbuild가 프로젝트 세팅 분석을 끝낸후에 시작되어 버립니다. 그래서 다운로드 받아진 안드로이드 라이브러리가 빌드된 앱에 포함되지 않습니다.
임시적인 해결법은 그대로 다시 빌드후 실행을 하면 동작합니다. 
두번째 빌드 부터는 이미 안드로이드 라이브러리 파일이 다운로드 되어 있기 때문에 프로젝트 세팅 분석에 파일이 정상적으로 포함됩니다.
클린빌드또는 안드로이드 라이브러리 파일이 삭제될수있는 과정을 진행하면 동일한 문제가 발생할 수 있습니다.

## 피드백 및 기여 (Feedback and Contribution)

개인적인 일정으로 인해 프로젝트를 빈번하게 확인하거나 들어온 이슈에 대처하고 반영하기가 어렵습니다. 이에 대해 양해를 부탁드리며, 사용자들이 마음껏 프로젝트를 가져가서 개선하고 발전시킬 수 있기를 바랍니다.

## 참고 레포지토리 (Referenced Repositories)

- [google_ml_kit_flutter](https://github.com/flutter-ml/google_ml_kit_flutter?tab=readme-ov-file): 각 플랫폼에서의 동작 코드 및 플랫폼 공통 코드는 google_ml_kit_flutter 라이브러의 코드를 기반으로 하였습니다.

## 사용한 라이브러리 (Used Libraries)

- [Google ML Kit](https://developers.google.com/ml-kit): Google ML Kit의 라이브러리 파일을 사용합니다.

## 라이선스 (License)

이 프로젝트는 MIT 라이선스 하에 배포됩니다. 자세한 내용은 [LICENSE](LICENSE) 파일을 참고하세요.

