using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class FrameRenderer : ViewRenderer<Frame, FrameView>
    {
        VisualElement _currentView;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new FrameView(new Rect(), string.Empty));
                }

                UpdateContent();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Frame.ContentProperty.PropertyName)
                UpdateContent();
        }

        void UpdateContent()
        {
            _currentView = Element.Content;
            var children = _currentView != null ? Platform.GetOrCreateRenderer(_currentView).GetNativeElement() : null;
            Control.Add(children);
        }
    }
}