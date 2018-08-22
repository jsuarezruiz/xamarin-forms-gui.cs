using System;
using System.ComponentModel;
using Terminal.Gui.Forms.Controls;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class LayoutRenderer : ViewRenderer<Layout, FormsPanel>
    {
        bool _isDisposed;

        IElementController ElementController => Element as IElementController;

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null) // Construct and SetNativeControl and suscribe control event
                {
                    var formsPanel = new FormsPanel
                    {
                        Element = Element
                    };

                    SetNativeControl(formsPanel);
                }

                // Update control property 
                UpdateClipToBounds();

                foreach (Element child in ElementController.LogicalChildren)
                {
                    HandleChildAdded(Element, new ElementEventArgs(child));
                }

                // Suscribe element event
                Element.ChildAdded += HandleChildAdded;
                Element.ChildRemoved += HandleChildRemoved;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Layout.IsClippedToBoundsProperty.PropertyName)
            {
                UpdateClipToBounds();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (Element != null)
                {
                    Element.ChildAdded -= HandleChildAdded;
                    Element.ChildRemoved -= HandleChildRemoved;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }

        void HandleChildAdded(object sender, ElementEventArgs e)
        {
            if (!(e.Element is VisualElement view))
            {
                return;
            }

            IVisualElementRenderer renderer;
            Platform.SetRenderer(view, renderer = Platform.CreateRenderer(view));
            Control.Add(renderer.GetNativeElement());
        }

        void HandleChildRemoved(object sender, ElementEventArgs e)
        {
            if (!(e.Element is VisualElement view))
            {
                return;
            }

            if (Platform.GetRenderer(view)?.GetNativeElement() is View native)
            {
                Control.Remove(native);
            }
        }

        void UpdateClipToBounds()
        {
            if (Element.IsClippedToBounds)
            {
                Control.Frame = new Rect(0, 0, Convert.ToInt32(Control.Element.Bounds.Width), Convert.ToInt32(Control.Element.Bounds.Height));
            }
        }
    }
}