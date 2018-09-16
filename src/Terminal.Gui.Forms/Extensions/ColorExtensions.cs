using System;

namespace Terminal.Gui.Forms.Extensions
{
    public static class ColorExtensions
    {
        public static ConsoleColor ToConsoleColor(this Xamarin.Forms.Color color)
        {
            int r = (int)(color.R * 255);
            int g = (int)(color.G * 255);
            int b = (int)(color.B * 255);
            var c = System.Drawing.Color.FromArgb(r, g, b);

            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
            index |= (c.R > 64) ? 4 : 0; // Red bit
            index |= (c.G > 64) ? 2 : 0; // Green bit
            index |= (c.B > 64) ? 1 : 0; // Blue bit

            return (ConsoleColor)index;
        }
    }
}