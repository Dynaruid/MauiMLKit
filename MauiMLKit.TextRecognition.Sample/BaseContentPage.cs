using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MLKitSharp.TextRecognition.Sample;

public abstract class BaseContentPage : ContentPage
{
    protected BaseContentPage(in bool shouldUseSafeArea = false)
    {
        On<iOS>().SetUseSafeArea(shouldUseSafeArea);
        On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}

public abstract class BaseContentPage<T> : BaseContentPage
    where T : ObservableObject
{
    protected BaseContentPage(in T viewModel, in bool shouldUseSafeArea = false)
        : base(shouldUseSafeArea)
    {
        base.BindingContext = viewModel;
    }

    protected new T BindingContext => (T)base.BindingContext;
}
