using Xamarin.Forms;

namespace Terminal.Gui.Forms.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoginBtn.Clicked += OnLoginBtnClicked;
        }

        private void OnLoginBtnClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Invalid credentials", "wrong username or password.", "Ok");
        }
    }
}