using MLKitSharp.TextRecognition.Sample.Pages.EntryPage;

namespace MLKitSharp.TextRecognition.Sample;

public partial class AppShell : Shell
{
    public AppShell(EntryPageView mainPage)
    {
        Items.Add(mainPage);
    }
}
