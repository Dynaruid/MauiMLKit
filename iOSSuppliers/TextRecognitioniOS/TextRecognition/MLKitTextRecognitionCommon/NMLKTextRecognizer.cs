using System;
using Foundation;
using MLKitVision;

namespace MLKitTextRecognitionCommon
{
    // 콜백 delegate 정의
    delegate void MLKTextRecognitionCallback(
        [NullAllowed] MLKText text,
        [NullAllowed] NSError error
    );

    [BaseType(typeof(NSObject))]
    interface MLKTextRecognizer
    {
        // 클래스 메소드를 C#에서 정적 메소드로 변환
        [Static]
        [Export("textRecognizerWithOptions:")]
        MLKTextRecognizer TextRecognizerWithOptions(MLKCommonTextRecognizerOptions options);

        // 이미지 처리 메소드

        [Export("processImage:completion:")]
        void ProcessImage(MLKCompatibleImage image, MLKTextRecognitionCallback completion);

        // 동기적 텍스트 인식 메소드
        [Export("resultsInImage:error:")]
        [return: NullAllowed]
        MLKText ResultsInImage(MLKCompatibleImage image, out NSError error);
    }
}
