using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class ButtonRenderer : ViewRenderer<Xamarin.Forms.Button, Gui.Button>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new Button(string.Empty));
                    Control.Clicked += HandleButtonClick;
                }

                UpdateText();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Button.TextProperty.PropertyName)
            {
                UpdateText();
            }
        }

        void UpdateText()
        {
            var newText = Element.Text;

            if (Control.Text != newText)
            {
                Control.Text = Element.Text;
            }
        }

        void HandleButtonClick()
        {
            if (Element is IButtonController buttonView)
            {
                buttonView.SendClicked();
            }
        }
    }
}