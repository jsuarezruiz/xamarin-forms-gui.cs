using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;

namespace Terminal.Gui.Forms.Renderers
{
    public class ProgressBarRenderer : ViewRenderer<Xamarin.Forms.ProgressBar, ProgressBar>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new ProgressBar());
                }

                UpdateProgress();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.ProgressBar.ProgressProperty.PropertyName)
            {
                UpdateProgress();
            }
        }

        void UpdateProgress()
        {
            if (Control == null)
            {
                return;
            }

            Control.Fraction = (float)Element.Progress;
        }
    }
}