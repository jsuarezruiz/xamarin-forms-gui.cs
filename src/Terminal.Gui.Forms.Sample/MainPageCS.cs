using Xamarin.Forms;

namespace Terminal.Gui.Forms.Sample
{
    public class MainPageCS : ContentPage
    {
        public MainPageCS()
        {
            var panel = new StackLayout();

            var userNameLabel = new Xamarin.Forms.Label
            {
                Text = "Username:"
            };
            panel.Children.Add(userNameLabel);

            var userNameEntry = new Entry();
            panel.Children.Add(userNameEntry);


            var passwordLabel = new Xamarin.Forms.Label
            {
                Text = "Password:"
            };
            panel.Children.Add(passwordLabel);

            var passwordEntry = new Entry
            {
                IsPassword = true
            };
            panel.Children.Add(passwordEntry);

            var loginButton = new Xamarin.Forms.Button
            {
                Text = "Login"
            };
            panel.Children.Add(loginButton);

            Content = panel;
        }
    }
}