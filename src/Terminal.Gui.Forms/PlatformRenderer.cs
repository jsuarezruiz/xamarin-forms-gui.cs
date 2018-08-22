using System;
using Xamarin.Forms;

namespace Terminal.Gui.Forms
{
    class PlatformRenderer : View, IDisposable
    {
        readonly Platform platform;

        public Platform Platform => platform;

        public PlatformRenderer(Platform platform)
        {
            this.platform = platform;
        }
    
        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Disconnect any events we started
                    // Inform the page it's leaving
                    if (Platform.Page is IPageController pc)
                    {
                        pc.SendDisappearing();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}