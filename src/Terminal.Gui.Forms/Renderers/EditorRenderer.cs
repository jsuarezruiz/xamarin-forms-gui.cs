using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class EditorRenderer : ViewRenderer<Editor, TextView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new TextView());
                }

                UpdateText();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Editor.TextProperty.PropertyName)
            {
                UpdateText();
            }
        }

        void UpdateText()
        {
            Control.Text = Element.Text ?? string.Empty;
        }
    }
}