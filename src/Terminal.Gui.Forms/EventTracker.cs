using System;
using Terminal.Gui.Forms.Renderers;

namespace Terminal.Gui.Forms
{
    public class EventTracker
    {
        readonly IVisualElementRenderer _renderer;
        bool _disposed;

        public EventTracker(IVisualElementRenderer renderer)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }
    }
}
