using System;
using Terminal.Gui.Forms.Renderers;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Controls
{
    public class FormsPanel : View
    {
        const int SizeConverter = 6;

        public FormsPanel() : base()
        {
          
        }

        public FormsPanel(Rect rect) : base(rect)
        {

        }

        IElementController ElementController => Layout as IElementController;

        public Layout Layout { get; set; }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            ArrangeSubviews();
        }

        public void ArrangeSubviews()
        {
            Layout.IsInNativeLayout = true;

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
                View nativeView = renderer.GetNativeElement();

                // Set child size
                var height = Convert.ToInt32(Math.Max(0, bounds.Height / SizeConverter));

                if (height <= 0)
                {
                    height = 1;
                }

                nativeView.Height = Dim.Sized(height);
                

                var width = Convert.ToInt32(Math.Max(0, bounds.Width / SizeConverter));
  
                if (width <= 0)
                {
                    width = 1;
                }

                nativeView.Width = Dim.Sized(width);
          
                // Set child position
                nativeView.X = Pos.At(Convert.ToInt32(bounds.X / SizeConverter));
                nativeView.Y = Pos.At(Convert.ToInt32(bounds.Y / SizeConverter));
            }

            Layout.IsInNativeLayout = false;
        }
    }
}