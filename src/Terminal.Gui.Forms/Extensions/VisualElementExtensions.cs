using System;
using Terminal.Gui.Forms.Renderers;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Extensions
{
    public static class VisualElementExtensions
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            IVisualElementRenderer renderer = Platform.GetRenderer(self);

            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(self);
                Platform.SetRenderer(self, renderer);
            }

            return renderer;
        }
    }
}