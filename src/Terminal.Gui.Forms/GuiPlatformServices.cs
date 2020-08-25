using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Terminal.Gui.Forms
{
    class GuiPlatformServices : IPlatformServices
    {
        public bool IsInvokeRequired => false;

        public string RuntimePlatform => "Gui";

        public OSAppTheme RequestedTheme => OSAppTheme.Unspecified;

        public void BeginInvokeOnMainThread(Action action)
        {
            Task.Run(action);
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public string GetMD5Hash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
        {
            switch (size)
            {
                default:
                case NamedSize.Default:
                    return 16;
                case NamedSize.Micro:
                    return 9;
                case NamedSize.Small:
                    return 12;
                case NamedSize.Medium:
                    return 22;
                case NamedSize.Large:
                    return 32;
            }
        }

        public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            return new HttpClient().GetStreamAsync(uri.ToString());
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            throw new NotImplementedException();
        }

        public void OpenUriAction(Uri uri)
        {
            Process.Start(uri.ToString());
        }

        public void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            Timer timer = null;
            timer = new Timer((_ =>
            {
                if (!callback())
                {
                    timer?.Dispose();
                    timer = null;
                }
            }), null, (int)interval.TotalMilliseconds, (int)interval.TotalMilliseconds);
        }

        public Ticker CreateTicker()
        {
            return new GuiTicker();
        }

        class GuiTicker : Ticker
        {
            Timer timer;
            protected override void DisableTimer()
            {
                var t = timer;
                timer = null;
                t?.Dispose();
            }
            protected override void EnableTimer()
            {
                if (timer != null)
                    return;
                var interval = TimeSpan.FromSeconds(1.0);

                timer = new Timer((_ =>
                {
                    this.SendSignals();
                }), null, (int)interval.TotalMilliseconds, (int)interval.TotalMilliseconds);
            }
        }

        public void QuitApplication()
        {
        }

        public Xamarin.Forms.Color GetNamedColor(String name)
        {
            Enum.TryParse<Xamarin.Forms.Color>(name, out var color);
            return color;
        }

        public SizeRequest GetNativeSize(VisualElement view, Double widthConstraint, Double heightConstraint)
        {
            return new SizeRequest();
        }
    }
}