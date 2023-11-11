using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.Content.StartPageContents
{
    public partial class StartPageMyKeysView : Grid
    {
        public StartPageMyKeysView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IndicatorViewProperty =
           BindableProperty.Create(
               propertyName: nameof(IndicatorView),
               returnType: typeof(IndicatorView),
               declaringType: typeof(StartPageMyKeysView),
               defaultBindingMode: BindingMode.Default,
               propertyChanged: IndicatorViewPropertyChanged);

        public IndicatorView IndicatorView
        {
            get => (IndicatorView)GetValue(IndicatorViewProperty);
            set => SetValue(IndicatorViewProperty, value);
        }

        static void IndicatorViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is StartPageMyKeysView template && newValue is IndicatorView indicatorView)
            {
                template.xCarouselView.IndicatorView = indicatorView;
            }
        }

    }
}