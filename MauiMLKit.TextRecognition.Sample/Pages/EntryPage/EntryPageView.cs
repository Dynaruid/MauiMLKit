using CommunityToolkit.Maui.Markup;
using MLKitSharp.TextRecognition.Sample.ViewUtilities;
using UraniumUI.Controls;

namespace MLKitSharp.TextRecognition.Sample.Pages.EntryPage;

public class EntryPageView : BaseContentPage<EntryPageViewModel>
{
    public EntryPageView(EntryPageViewModel mainViewModel)
        : base(mainViewModel, true)
    {
        Content = new VerticalStackLayout
        {
            Spacing = 4,
            Padding = 10,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Label
                {
                    Text = "Language to detect",
                    FontSize = 20,
                    HorizontalTextAlignment = TextAlignment.Center,
                },
                new Picker
                {
                    ItemsSource = (System.Collections.IList)mainViewModel.RecognitionLanguages.Keys,
                    FontSize = 20,
                    TextColor = Colors.Black,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 150,
                }.Bind(
                    Picker.SelectedItemProperty,
                    nameof(mainViewModel.SelectedRecognitionLanguages),
                    BindingMode.TwoWay
                ),
                new BoxView { HeightRequest = 6, }.CenterHorizontal(),
                new ConditionalRenderer
                {
                    TrueElement = new VerticalStackLayout
                    {
                        Children =
                        {
                            new Frame
                            {
                                BackgroundColor = Colors.White,
                                HeightRequest = 300,
                                CornerRadius = 10,
                                Shadow = new Shadow
                                {
                                    Brush = Colors.DarkGray,
                                    Offset = new Point(0, 5),
                                },
                                Content = new Image().Bind(
                                    Image.SourceProperty,
                                    static (EntryPageViewModel vm) => vm.CurrentImageSource
                                ),
                                HorizontalOptions = LayoutOptions.Fill,
                            },
                            new BoxView { HeightRequest = 6, }.CenterHorizontal(),
                            new SelectableLabel
                            {
                                MaximumHeightRequest = 250,
                                HorizontalOptions = LayoutOptions.Fill,
                                FontSize = 18,
                                TextColor = Colors.Black,
                                HorizontalTextAlignment = TextAlignment.Center,
                            }.Bind(
                                SelectableLabel.TextProperty,
                                static (EntryPageViewModel vm) => vm.ImageText
                            )
                        }
                    },
                    FalseElement = new VerticalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Please Pick an Image...",
                                FontSize = 25,
                                HorizontalTextAlignment = TextAlignment.Center,
                            },
                            new BoxView { HeightRequest = 10, }.CenterHorizontal(),
                        }
                    }
                }.Bind(
                    ConditionalRenderer.ConditionProperty,
                    nameof(mainViewModel.IsImageLoaded),
                    mode: BindingMode.OneWay
                ),
                new BoxView { HeightRequest = 7, }.CenterHorizontal(),
                new Frame
                {
                    Content = new Button
                    {
                        BackgroundColor = Colors.Transparent,
                        WidthRequest = 220,
                        HeightRequest = 60,
                        Text = "Load Image",
                        TextColor = Colors.AliceBlue,
                        FontSize = 27,
                    }
                        .Font(bold: true)
                        .Bind(
                            Button.CommandProperty,
                            static (EntryPageViewModel vm) => vm.LoadImageCommand,
                            mode: BindingMode.OneTime
                        ),
                    Background = new LinearGradientBrush
                    {
                        GradientStops =
                        {
                            new GradientStop { Color = Colors.BlueViolet, Offset = 0.3f },
                            new GradientStop { Color = Colors.LightBlue, Offset = 1.0f }
                        }
                    },
                    Padding = Thickness.Zero,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    CornerRadius = 15,
                    Shadow = new Shadow { Brush = Colors.DarkGray, Offset = new Point(0, 5), },
                }
            }
        };
    }
}
