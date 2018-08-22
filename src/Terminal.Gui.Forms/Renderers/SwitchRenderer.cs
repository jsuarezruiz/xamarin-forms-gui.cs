using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class SwitchRenderer : ViewRenderer<Xamarin.Forms.Switch, CheckBox>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new CheckBox(string.Empty));
                    Control.Toggled += OnNativeToggled;
                }

                UpdateIsToggled();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Switch.IsToggledProperty.PropertyName)
            {
                UpdateIsToggled();
            }
        }

        void UpdateIsToggled()
        {
            Control.Checked = Element.IsToggled;
        }

        private void OnNativeToggled(object sender, System.EventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(Switch.IsToggledProperty, Control.Checked);
        }
    }
}