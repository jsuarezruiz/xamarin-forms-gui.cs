using System;
using Terminal.Gui.Forms.Renderers;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Controls
{
    public class FormsPanel : View
    {
        public FormsPanel() : base()
        {
            LayoutStyle = LayoutStyle.Computed;
        }

        public FormsPanel(Rect rect) : base(rect)
        {

        }

        IElementController ElementController => Element as IElementController;

        public Layout Element { get; set; }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            ArrangeSubviews();
        }

        public void ArrangeSubviews()
        {
            int temp = 0;   // TODO: Review Layout!

            Element.IsInNativeLayout = true;

            for (var i = 0; i < ElementController.LogicalChildren.Count; i++)
            {
                if (!(ElementController.LogicalChildren[i] is VisualElement child))
                {
                    continue;
                }

                IVisualElementRenderer renderer = Platform.GetRenderer(child);

                if (renderer == null)
                {
                    continue;
                }

                Rectangle bounds = child.Bounds;

                // Set child size
                var height = Convert.ToInt32(Math.Max(0, bounds.Height));

                if (height > 0)
                {
                    renderer.GetNativeElement().Height = Dim.Sized(height);
                }

                var width = Convert.ToInt32(Math.Max(0, bounds.Width));

                if (width > 0)
                {
                    renderer.GetNativeElement().Width = Dim.Sized(width);
                }

                // Set child position
                renderer.GetNativeElement().X = Convert.ToInt32(bounds.X);
                renderer.GetNativeElement().Y = Convert.ToInt32(bounds.Y + temp);

                temp++;
            }

            Element.IsInNativeLayout = false;
        }
    }
}