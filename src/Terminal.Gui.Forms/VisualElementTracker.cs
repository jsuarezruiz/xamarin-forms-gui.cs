using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Terminal.Gui.Forms
{
    public abstract class VisualElementTracker : IDisposable
    {
        public abstract void Dispose();

        public event EventHandler Updated;

        protected void OnUpdated()
        {
            Updated?.Invoke(this, System.EventArgs.Empty);
        }
    }

    public class VisualElementTracker<TElement, TNativeElement> : VisualElementTracker where TElement : VisualElement where TNativeElement : Gui.View
    {
        bool _disposed;
        TNativeElement _control;
        TElement _element;

        bool _invalidateArrangeNeeded;

        public TNativeElement Control
        {
            get { return _control; }
            set
            {
                _control = value;
                UpdateNativeControl();
            }
        }

        public TElement Element
        {
            get { return _element; }
            set
            {
                if (_element == value)
                    return;

                if (_element != null)
                {
                    _element.BatchCommitted -= HandleRedrawNeeded;
                    _element.PropertyChanged -= HandlePropertyChanged;
                }

                _element = value;

                if (_element != null)
                {
                    _element.BatchCommitted += HandleRedrawNeeded;
                    _element.PropertyChanged += HandlePropertyChanged;
                }

                UpdateNativeControl();
            }
        }


        protected virtual void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element.Batched)
            {
                if (e.PropertyName == VisualElement.XProperty.PropertyName ||
                    e.PropertyName == VisualElement.YProperty.PropertyName ||
                    e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                    e.PropertyName == VisualElement.HeightProperty.PropertyName)
                {
                    _invalidateArrangeNeeded = true;
                }

                return;
            }

            if (e.PropertyName == VisualElement.XProperty.PropertyName ||
                e.PropertyName == VisualElement.YProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                MaybeInvalidate();
            }
        }

        protected virtual void UpdateNativeControl()
        {
            if (Element == null || Control == null)
                return;

            if (_invalidateArrangeNeeded)
            {
                MaybeInvalidate();
            }

            _invalidateArrangeNeeded = false;

            OnUpdated();
        }

        void HandleRedrawNeeded(object sender, System.EventArgs e)
        {
            UpdateNativeControl();
        }

        void MaybeInvalidate()
        {
            if (Element.IsInNativeLayout)
            {
                return;
            }

            var parent = Control.SuperView;
            parent?.LayoutSubviews();
            Control.LayoutSubviews();
        }

        public override void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (_element != null)
            {
                _element.BatchCommitted -= HandleRedrawNeeded;
                _element.PropertyChanged -= HandlePropertyChanged;
            }

            Element = null;
        }
    }
}