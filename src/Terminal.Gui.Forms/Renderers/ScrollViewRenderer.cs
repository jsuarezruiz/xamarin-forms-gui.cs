using System;
using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Terminal.Gui.Forms.Extensions;

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
                    Control.ContentOffset = new Point(0, 0);
                    Control.ShowVerticalScrollIndicator = true;
                    Control.ShowHorizontalScrollIndicator = true;
                }

                LoadContent();
                UpdateContentSize();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.ScrollView.ContentSizeProperty.PropertyName)
                UpdateContentSize();
            else if (e.PropertyName == nameof(Xamarin.Forms.ScrollView.Content))
                LoadContent();
        }

        private void UpdateContentSize()
        {
            if (Control == null)
                return;

            var contentSize = Element.ContentSize;

            var height = Convert.ToInt32(contentSize.Height);
            var width = Convert.ToInt32(contentSize.Width);

            Control.ContentSize = new Size(width, height);
        }

        private void LoadContent()
        {
            var currentView = Element.Content;

            IVisualElementRenderer renderer = null;

            if (currentView != null)
            {
                renderer = currentView.GetOrCreateRenderer();
            }

            if (renderer != null)
            {
                var content = renderer.GetNativeElement();
                
                Control.Add(content);
            }
        }
    }
}