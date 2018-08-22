namespace Terminal.Gui.Forms.Sample
{
    public class Program
    {
        public static void Main()
        {
            Application.Init();
            Forms.Init();
            var app = new App();
            var window = new FormsWindow("Xamarin.Forms gui.cs Backend");
            window.LoadApplication(app);
            Application.Run();
        }
    }
}