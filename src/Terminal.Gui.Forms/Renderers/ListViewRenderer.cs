using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Terminal.Gui.Forms.EventArgs;
using Xamarin.Forms;

namespace Terminal.Gui.Forms.Renderers
{
    public class ListViewRenderer : ViewRenderer<Xamarin.Forms.ListView, ListView>
    {
        private List<string> _source;

        public ListViewRenderer()
        {
            _source = new List<string>();
        }

        Xamarin.Forms.ListView ListView => Element;

        IListViewController Controller => Element;

        ITemplatedItemsView<Cell> TemplatedItemsView => Element;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var templatedItems = TemplatedItemsView.TemplatedItems;
                templatedItems.CollectionChanged -= OnCollectionChanged;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new ListView(new List<string>()));
                }

                var templatedItems = TemplatedItemsView.TemplatedItems;
                templatedItems.CollectionChanged += OnCollectionChanged;

                UpdateItems();
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            var items = TemplatedItemsView.TemplatedItems;

            if (!items.Any())
            {
                return;
            }

            bool grouping = Element.IsGroupingEnabled;

            if (grouping)
            {
                // Not supported!
                return;
            }

            foreach (var item in items)
            {
                if (item is TextCell cell)
                {
                    _source.Add(cell.Text);
                }
            }

            Control.SetSource(_source);
        }
    }
}