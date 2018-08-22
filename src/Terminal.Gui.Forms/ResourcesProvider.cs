using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Terminal.Gui.Forms
{
    public class ResourcesProvider : ISystemResourcesProvider
    {
        public IResourceDictionary GetSystemResources()
        {
            return new ResourceDictionary();
        }
    }
}