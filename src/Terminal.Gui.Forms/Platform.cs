using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terminal.Gui.Forms.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Terminal.Gui.Forms
{
    class Platform : BindableObject, INavigation, IDisposable
    {
        bool _disposed;

        readonly PlatformRenderer _renderer;

        public View Element => _renderer;

        public Page Page { get; private set; }

        IReadOnlyList<Page> INavigation.ModalStack => throw new NotImplementedException();

        IReadOnlyList<Page> INavigation.NavigationStack => throw new NotImplementedException();

        public static readonly BindableProperty RendererProperty = BindableProperty.CreateAttached("Renderer", typeof(IVisualElementRenderer), typeof(Platform), default(IVisualElementRenderer),
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                if (bindable is VisualElement view)
                {
                    view.IsPlatformEnabled = newvalue != null;
                }
            });

        public Platform()
        {
            _renderer = new PlatformRenderer(this);

            SubscribeAlertsAndActionSheets();
        }

        internal void SubscribeAlertsAndActionSheets()
        {
            MessagingCenter.Subscribe<Page, AlertArguments>(this, Page.AlertSignalName, OnPageAlert);
        }

        void OnPageAlert(Page sender, AlertArguments options)
        {
            string content = options.Message ?? options.Title ?? string.Empty;

            if (options.Message == null || options.Title == null)
            {
                MessageBox.Query(48, 6, string.Empty, content, new string[] { options.Cancel });
            }
            else
            {
                var buttons = new string[] { options.Cancel };

                if (!string.IsNullOrEmpty(options.Accept))
                {
                    buttons = new string[] { options.Accept, options.Cancel };
                }

                MessageBox.Query(48, 6, options.Title, content, buttons);
            }
        }

        void IDisposable.Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            MessagingCenter.Unsubscribe<Page, bool>(this, Page.BusySetSignalName);

            DisposeModelAndChildrenRenderers(Page);
        }
        public static IVisualElementRenderer GetOrCreateRenderer(VisualElement element)
        {
            if (GetRenderer(element) == null)
                SetRenderer(element, CreateRenderer(element));

            return GetRenderer(element);
        }

        public static IVisualElementRenderer CreateRenderer(VisualElement element)
        {
            var renderer = Registrar.Registered.GetHandler<IVisualElementRenderer>(element.GetType()) ?? new DefaultViewRenderer();
            renderer.SetElement(element);

            return renderer;
        }

        public static IVisualElementRenderer GetRenderer(VisualElement bindable)
        {
            return (IVisualElementRenderer)bindable.GetValue(RendererProperty);
        }

        public static void SetRenderer(VisualElement bindable, IVisualElementRenderer value)
        {
            bindable.SetValue(RendererProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            SetInheritedBindingContext(Page, BindingContext);

            base.OnBindingContextChanged();
        }

        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            var renderView = GetRenderer(view);

            if (renderView == null || renderView.GetNativeElement() == null)
                return new SizeRequest(Xamarin.Forms.Size.Zero);

            return renderView.GetDesiredSize(widthConstraint, heightConstraint);
        }

        public void SetPage(Page newRoot)
        {
            if (newRoot == null)
            {
                return;
            }

            if (Page != null)
            {
                throw new NotImplementedException();
            }

            Page = newRoot;

            AddChild(Page);

            Page.DescendantRemoved += HandleChildRemoved;

            Xamarin.Forms.Application.Current.NavigationProxy.Inner = this;
        }

        void HandleChildRemoved(object sender, ElementEventArgs e)
        {
            var view = e.Element;
            DisposeModelAndChildrenRenderers(view);
        }

        void DisposeModelAndChildrenRenderers(Element view)
        {
            IVisualElementRenderer renderer;
            foreach (VisualElement child in view.Descendants())
            {
                renderer = GetRenderer(child);
                child.ClearValue(RendererProperty);

                if (renderer != null)
                {
                    renderer.Dispose();
                }
            }

            renderer = GetRenderer((VisualElement)view);

            if (renderer != null)
            {
                renderer.Dispose();
            }

            view.ClearValue(RendererProperty);
        }

        void AddChild(VisualElement view)
        {
            if (!Xamarin.Forms.Application.IsApplicationOrNull(view.RealParent))
                System.Diagnostics.Debug.WriteLine("Tried to add parented view to canvas directly");

            if (GetRenderer(view) == null)
            {
                var viewRenderer = CreateRenderer(view);
                SetRenderer(view, viewRenderer);

                _renderer.Add(viewRenderer.GetNativeElement());
                viewRenderer.SetElementSize(new Size(640, 480));
            }
            else
                System.Diagnostics.Debug.WriteLine("Potential view double add");
        }

        void INavigation.InsertPageBefore(Page page, Page before)
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopAsync()
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopModalAsync()
        {
            throw new NotImplementedException();
        }

        Task<Page> INavigation.PopModalAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        Task INavigation.PopToRootAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushAsync(Page page)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        Task INavigation.PushModalAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        void INavigation.RemovePage(Page page)
        {
            throw new NotImplementedException();
        }
    }
}