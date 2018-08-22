using Terminal.Gui.Forms.EventArgs;

namespace Terminal.Gui.Forms.Renderers
{
    public class DefaultViewRenderer : ViewRenderer<Xamarin.Forms.View, View>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            SetNativeControl(new View());
        }
    }
}