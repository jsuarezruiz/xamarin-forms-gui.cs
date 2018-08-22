using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class VisualPageRenderer<TElement, TNativeElement> : ViewRenderer<TElement, TNativeElement>
          where TElement : Page
          where TNativeElement : View
    {
        bool _isDisposed;

        protected override void OnElementChanged(ElementChangedEventArgs<TElement> e)
        {
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            base.Dispose(disposing);
        }
    }
}