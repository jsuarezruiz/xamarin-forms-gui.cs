using System;
using System.IO;
using System.Reflection;
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
            throw new NotImplementedException();
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

        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            return new SizeRequest()
            {
                Minimum = new Xamarin.Forms.Size(view.MinimumWidthRequest, view.MinimumHeightRequest),
                Request = new Xamarin.Forms.Size(view.WidthRequest, view.HeightRequest),
            };
        }

        public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            throw new NotImplementedException();
        }

        public void OpenUriAction(Uri uri)
        {
            throw new NotImplementedException();
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
    }
}