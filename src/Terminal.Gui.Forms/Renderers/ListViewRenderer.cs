using System.Collections.Generic;
using Terminal.Gui.Forms.EventArgs;

namespace Terminal.Gui.Forms.Renderers
{
    public class ListViewRenderer : ViewRenderer<Xamarin.Forms.ListView, ListView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new ListView(new List<string>()));
                }
            }
        }
    }
}