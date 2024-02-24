
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.Content.StartPageContents
{
    public partial class StartPageMyKeysCryptoView : Grid
    {
        public StartPageMyKeysCryptoView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IndicatorViewProperty =
           BindableProperty.Create(
               propertyName: nameof(IndicatorView),
               returnType: typeof(IndicatorView),
               declaringType: typeof(StartPageMyKeysCryptoView),
               defaultBindingMode: BindingMode.Default,
               propertyChanged: IndicatorViewPropertyChanged);

        public IndicatorView IndicatorView
        {
            get => (IndicatorView)GetValue(IndicatorViewProperty);
            set => SetValue(IndicatorViewProperty, value);
        }

        static void IndicatorViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is StartPageMyKeysCryptoView template && newValue is IndicatorView indicatorView)
            {
                template.xCarouselCryptoView.IndicatorView = indicatorView;
            }
        }
    }
}