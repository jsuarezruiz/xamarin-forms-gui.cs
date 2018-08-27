using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public interface IVisualElementRenderer : IRegisterable, IDisposable
    {
        View GetNativeElement();

        VisualElement Element { get; }

        event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        void SetElementSize(Size size);

        SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);

        void SetElement(VisualElement element);
    }

    public class ViewRenderer<TElement, TNativeElement> : IVisualElementRenderer
       where TElement : VisualElement where TNativeElement : Gui.View
    {
        readonly List<EventHandler<VisualElementChangedEventArgs>> _elementChangedHandlers =
            new List<EventHandler<VisualElementChangedEventArgs>>();

        VisualElementTracker _tracker;

        IElementController ElementController => Element as IElementController;

        public TNativeElement Control { get; private set; }

        public TElement Element { get; private set; }

        protected virtual bool AutoTrack { get; set; } = true;

        protected VisualElementTracker Tracker
        {
            get { return _tracker; }
            set
            {
                if (_tracker == value)
                    return;

                if (_tracker != null)
                {
                    _tracker.Dispose();
                    _tracker.Updated -= HandleTrackerUpdated;
                }

                _tracker = value;

                if (_tracker != null)
                {
                    _tracker.Updated += HandleTrackerUpdated;
                }
            }
        }

        VisualElement IVisualElementRenderer.Element
        {
            get { return Element; }
        }

        public View GetNativeElement()
        {
            return Control;
        }

        event EventHandler<VisualElementChangedEventArgs> IVisualElementRenderer.ElementChanged
        {
            add { _elementChangedHandlers.Add(value); }
            remove { _elementChangedHandlers.Remove(value); }
        }

        public virtual SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest();
        }

        public void SetElementSize(Size size)
        {
            Layout.LayoutChildIntoBoundingRegion(Element, new Rectangle(Element.X, Element.Y, size.Width, size.Height));
        }

        public void SetElement(VisualElement element)
        {
            TElement oldElement = Element;
            Element = (TElement)element;

            if (oldElement != null)
            {
                oldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            Element.PropertyChanged += OnElementPropertyChanged;

            OnElementChanged(new ElementChangedEventArgs<TElement>(oldElement, Element));
        }

        public event EventHandler<ElementChangedEventArgs<TElement>> ElementChanged;

        protected virtual void OnElementChanged(ElementChangedEventArgs<TElement> e)
        {
            var args = new VisualElementChangedEventArgs(e.OldElement, e.NewElement);

            for (var i = 0; i < _elementChangedHandlers.Count; i++)
                _elementChangedHandlers[i](this, args);

            ElementChanged?.Invoke(this, e);
        }

        protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                UpdateHeight();
            }
            else if (e.PropertyName == VisualElement.WidthProperty.PropertyName)
            {
                UpdateWidth();
            }
        }

        protected void SetNativeControl(TNativeElement native)
        {
            Control = native;

            if (AutoTrack && Tracker == null)
            {
                Tracker = new VisualElementTracker<TElement, View> { Element = Element, Control = Control };
            }
        }

        protected virtual void UpdateHeight()
        {
            if (Control == null || Element == null)
            {
                return;
            }

            Control.Height = Dim.Sized(Element.Height > 0 ? Convert.ToInt32(Element.Height) : 0);
        }

        protected virtual void UpdateWidth()
        {
            if (Control == null || Element == null)
            {
                return;
            }

            Control.Width = Dim.Sized(Element.Width > 0 ? Convert.ToInt32(Element.Width) : 0);
        }


        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
                return;

            _disposed = true;

            if (Element != null)
            {
                Element.PropertyChanged -= OnElementPropertyChanged;
            }

            Tracker = null;
        }

        void HandleTrackerUpdated(object sender, System.EventArgs e)
        {
            UpdateNativeWidget();
        }

        protected virtual void UpdateNativeWidget()
        {
            UpdateEnabled();
        }

        void UpdateEnabled()
        {
            // TODO:
        }
    }
}