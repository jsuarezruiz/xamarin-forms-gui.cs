using Terminal.Gui.Forms.EventArgs;

namespace Terminal.Gui.Forms.Renderers
{
    public class ScrollViewRenderer : ViewRenderer<Xamarin.Forms.ScrollView, ScrollView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ScrollView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new ScrollView(Rect.Empty));
                }
            }
        }
    }
}