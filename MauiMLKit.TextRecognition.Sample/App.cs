namespace MLKitSharp.TextRecognition.Sample;

public partial class App : Application
{
    public App(AppShell shell)
    {
        var colorsDictionary = new global::Resources.Styles.Colors();
        var stylesDictionary = new global::Resources.Styles.Styles();
        var materialStyles = new UraniumUI.Material.Resources.StyleResource
        {
            ColorsOverride = colorsDictionary,
            BasedOn = stylesDictionary
        };
        Resources.MergedDictionaries.Add(colorsDictionary);
        Resources.MergedDictionaries.Add(stylesDictionary);
        Resources.MergedDictionaries.Add(materialStyles);
        MainPage = shell;
    }
}
