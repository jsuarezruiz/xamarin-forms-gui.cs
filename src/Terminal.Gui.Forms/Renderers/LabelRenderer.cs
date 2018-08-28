using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;

namespace Terminal.Gui.Forms.Renderers
{
    public class LabelRenderer : ViewRenderer<Xamarin.Forms.Label, Label>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new Label(string.Empty));
                }

                UpdateText();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Label.TextProperty.PropertyName)
            {
                UpdateText();
            }
        }

        void UpdateText()
        {
            var newText = Element.Text;

            if(newText == null)
            {
                return;
            }

            if (Control.Text != newText)
            {
                Control.Text = Element.Text;
            }
        }
    }
}