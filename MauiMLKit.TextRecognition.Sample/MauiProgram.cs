using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Logging;
using MLKitSharp.TextRecognition.Sample.Pages.EntryPage;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;

namespace MLKitSharp.TextRecognition.Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .UseSkiaSharp()
            .UseUraniumUI()
            .UseUraniumUIMaterial();

        builder.Services.AddSingleton<App>();
        builder.Services.AddSingleton<AppShell>();

        builder.Services.AddTransientWithShellRoute<EntryPageView, EntryPageViewModel>(
            $"//{nameof(EntryPageView)}"
        );
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
