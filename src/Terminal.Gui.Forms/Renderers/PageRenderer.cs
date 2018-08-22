using System.ComponentModel;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class PageRenderer : VisualPageRenderer<Page, View>
    {
        VisualElement _currentView;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null) // Construct and SetNativeControl and suscribe control event
                {
                    SetNativeControl(new View());
                }

                // Update control property 
                UpdateContent();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ContentPage.ContentProperty.PropertyName)
                UpdateContent();
        }

        void UpdateContent()
        {
            if (Element is ContentPage page)
            {
                _currentView = page.Content;
                var content = _currentView != null ? Platform.GetOrCreateRenderer(_currentView).GetNativeElement() : null;

                Control.Add(content);
                FormsWindow.Current.Add(content);
            }
        }
    }
}