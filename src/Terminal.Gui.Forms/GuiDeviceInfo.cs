using Xamarin.Forms.Internals;

namespace Terminal.Gui.Forms
{
    class GuiDeviceInfo : DeviceInfo
    {
        public override Xamarin.Forms.Size PixelScreenSize => new Xamarin.Forms.Size(640, 480);

        public override Xamarin.Forms.Size ScaledScreenSize => PixelScreenSize;

        public override double ScalingFactor => 1;
    }
}