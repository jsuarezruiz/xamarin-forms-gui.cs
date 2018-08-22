using System;
using System.ComponentModel;

namespace Terminal.Gui.Forms
{
    public class FormsWindow : Window
    {
        private Xamarin.Forms.Application _application;

        public FormsWindow(string title) : base(title)
        {

        }

        public static FormsWindow Current { get; private set; }

        public void LoadApplication(Xamarin.Forms.Application application)
        {
            Current = this;
            Application.Top.Add(Current);

            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            Xamarin.Forms.Application.SetCurrentApplication(application);
            _application = application;

            application.PropertyChanged += ApplicationOnPropertyChanged;
            UpdateMainPage();
        }

        private void ApplicationOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Xamarin.Forms.Application.MainPage))
            {
                UpdateMainPage();
            }
        }

        private void UpdateMainPage()
        {
            if (_application.MainPage == null)
            {
                return;
            }

            var platform = new Platform();

            platform.SetPage(_application.MainPage);
        }
    }
}