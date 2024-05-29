using MLKitTextRecognitionCommon;

namespace MLKitSharp.TextRecognition;

public class RecognizedText(string text, List<TextBlock> blocks, int hasError = 0)
{
    public string Text { get; private set; } = text;
    public List<TextBlock> Blocks { get; private set; } = blocks;
    public int HasError { get; set; } = hasError;

    public static RecognizedText FromMLKText(MLKText mlkText)
    {
        string resText = mlkText.Text;
        var textBlocks = new List<TextBlock>();

        foreach (MLKTextBlock block in mlkText.Blocks)
        {
            TextBlock textBlock = TextBlock.FromMLKTextBlock(block);

            textBlocks.Add(textBlock);
        }

        return new RecognizedText(text: resText, blocks: textBlocks);
    }

    public static RecognizedText FromEmpty()
    {
        return new RecognizedText(text: "", blocks: []);
    }

    public static RecognizedText FromError1()
    {
        return new RecognizedText(text: "", blocks: [], hasError: 1);
    }
}
