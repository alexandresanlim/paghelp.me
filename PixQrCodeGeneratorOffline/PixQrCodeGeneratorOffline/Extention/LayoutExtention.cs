using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class LayoutExtention
    {
        public static Task RunOpacityAnimationAsync(this VisualElement visualElement)
        {
            try
            {
                visualElement.Opacity = 0;
                return visualElement.FadeTo(1, 1000);
            }
            catch (Exception ex)
            {
                ex.SendToLog();
                return Task.CompletedTask;
            }
        }


        //public static Grid SetLoad(this View content, StackLayoutLoad load)
        //{
        //    return new Grid
        //    {
        //        VerticalOptions = LayoutOptions.FillAndExpand,
        //        Children =
        //        {
        //            content,
        //            load
        //        }
        //    };
        //}

        //public static ScrollView SetOnScrollView(this View content)
        //{
        //    return new ScrollView
        //    {
        //        HorizontalOptions = LayoutOptions.FillAndExpand,
        //        VerticalOptions = LayoutOptions.FillAndExpand,
        //        Content = content
        //    };
        //}
    }
}
