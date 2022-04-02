
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public partial class TagInfo : Frame
    {
        public TagInfo()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TypeProperty =
           BindableProperty.Create(propertyName: nameof(Type),
                               returnType: typeof(TagInfoType),
                               declaringType: typeof(TagInfo),
                               defaultValue: null,
                               defaultBindingMode: BindingMode.Default,
                               propertyChanged: TypePropertyChanged);
        public TagInfoType Type
        {
            get => (TagInfoType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        static void TypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TagInfo control && newValue is TagInfoType value)
            {
                switch (value)
                {
                    case TagInfoType.New:
                        control.BackgroundColor = Color.LimeGreen;
                        control.lbTitle.Text = "Novo";
                        break;
                    case TagInfoType.Beta:
                    default:
                        control.BackgroundColor = Color.Yellow;
                        control.lbTitle.Text = "Beta";
                        break;
                }
            }
        }
    }

    public enum TagInfoType
    {
        Beta,
        New
    }
}