using ContentView = Microsoft.Maui.Controls.ContentView;

namespace MLKitSharp.TextRecognition.Sample.ViewUtilities;

public partial class ConditionalRenderer : ContentView
{
    public static readonly BindableProperty ConditionProperty = BindableProperty.Create(
        nameof(Condition),
        typeof(bool),
        typeof(ConditionalRenderer),
        propertyChanged: OnContentDependentPropertyChanged
    );

    public bool Condition
    {
        get => (bool)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly BindableProperty TrueProperty = BindableProperty.Create(
        nameof(TrueElement),
        typeof(View),
        typeof(ConditionalRenderer),
        propertyChanged: OnContentDependentPropertyChanged
    );

    public View TrueElement
    {
        get => (View)GetValue(TrueProperty);
        set => SetValue(TrueProperty, value);
    }

    public static readonly BindableProperty FalseProperty = BindableProperty.Create(
        nameof(FalseElement),
        typeof(View),
        typeof(ConditionalRenderer),
        propertyChanged: OnContentDependentPropertyChanged
    );

    public View FalseElement
    {
        get => (View)GetValue(FalseProperty);
        set => SetValue(FalseProperty, value);
    }

    private static void OnContentDependentPropertyChanged(
        BindableObject d,
        object oldValue,
        object newValue
    )
    {
        var control = (ConditionalRenderer)d;
        control.UpdateContent();
    }

    private void UpdateContent()
    {
        Content = Condition ? TrueElement : FalseElement;
    }
}
