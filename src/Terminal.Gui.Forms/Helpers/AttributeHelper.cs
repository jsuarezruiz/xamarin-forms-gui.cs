using System;

namespace Terminal.Gui.Forms.Helpers
{
    public class AttributeHelper
    {
        public static Attribute MakeColor(ConsoleColor f, ConsoleColor b)
        {
            return new Attribute(((int)f | (int)b << 4));
        }
    }
}
