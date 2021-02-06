using System;
using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Terminal.Gui.Forms.Extensions;
using Terminal.Gui.Forms.Helpers;

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
                UpdateTextColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Label.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Xamarin.Forms.Label.TextColorProperty.PropertyName)
                UpdateTextColor();
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

        void UpdateTextColor()
        {
            var textColor = Element.TextColor != Xamarin.Forms.Color.Default ? Element.TextColor : Xamarin.Forms.Color.Black;

            var consoleColor = textColor.ToConsoleColor();

            Control.ColorScheme = new ColorScheme()
            {
                Normal = AttributeHelper.MakeColor(consoleColor, ConsoleColor.Blue)
            };
        }
    }
}