using Xamarin.Forms;

namespace Terminal.Gui.Forms.EventArgs
{
    public class VisualElementChangedEventArgs : ElementChangedEventArgs<VisualElement>
    {
        public VisualElementChangedEventArgs(VisualElement oldElement, VisualElement newElement)
            : base(oldElement, newElement)
        {
        }
    }

    public class ElementChangedEventArgs<TElement> : System.EventArgs where TElement : Xamarin.Forms.Element
    {
        public ElementChangedEventArgs(TElement oldElement, TElement newElement)
        {
            OldElement = oldElement;
            NewElement = newElement;
        }

        public TElement NewElement { get; private set; }

        public TElement OldElement { get; private set; }
    }
}