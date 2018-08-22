using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Terminal.Gui.Forms
{
    public static class Forms
    {
        public static bool IsInitialized { get; private set; }

        public static void Init()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;

            Log.Listeners.Add(new DelegateLogListener((c, m) => System.Diagnostics.Debug.WriteLine(m, c)));

            Device.SetIdiom(TargetIdiom.Desktop);
            Device.PlatformServices = new GuiPlatformServices();
            Device.Info = new GuiDeviceInfo();
            Xamarin.Forms.Color.SetAccent(Xamarin.Forms.Color.Black);

            Registrar.RegisterAll(new[]
            {
                typeof(ExportRendererAttribute)
            });
        }
    }
}