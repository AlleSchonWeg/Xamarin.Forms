﻿using System.Maui.Controls.Primitives;
#if __IOS__
using NativeView = UIKit.UIView;
#elif __MACOS__
using NativeView = AppKit.NSView;
#elif MONOANDROID
using NativeView = Android.Views.View;
#elif NETCOREAPP
using NativeView = System.Windows.FrameworkElement;
#elif NETSTANDARD
using NativeView = System.Object;
#endif

namespace System.Maui.Platform
{
	public abstract partial class AbstractViewHandler<TVirtualView, TNativeView> : IViewHandler
		where TVirtualView : class, IView
#if !NETSTANDARD
		where TNativeView : NativeView
#endif
	{
        protected readonly PropertyMapper defaultMapper;
		protected PropertyMapper mapper;

		protected AbstractViewHandler(PropertyMapper mapper)
		{
			defaultMapper = mapper;
		}

		protected abstract TNativeView CreateView();

		public NativeView View => HasContainer ? (NativeView)ContainerView : TypedNativeView;
		public TNativeView TypedNativeView { get; private set; }
		protected TVirtualView VirtualView { get; private set; }
		public object NativeView => TypedNativeView;
		static bool HasSetDefaults;

		public virtual void SetView(IView view)
		{
			VirtualView = view as TVirtualView;
			TypedNativeView ??= CreateView();

			if (!HasSetDefaults)
			{
				SetupDefaults();
				HasSetDefaults = true;
			}

			mapper = defaultMapper;

			if (VirtualView is IPropertyMapperView imv)
			{
				var map = imv.GetPropertyMapperOverrides();
				var instancePropertyMapper = map as PropertyMapper<TVirtualView>;
				if (map != null && instancePropertyMapper == null)
				{
				}
				if (instancePropertyMapper != null)
				{
					instancePropertyMapper.Chained = defaultMapper;
					mapper = instancePropertyMapper;
				}
			}

			mapper?.UpdateProperties(this, VirtualView);
		}

		public virtual void Remove(IView view)
		{
			VirtualView = null;
		}

		protected virtual void DisposeView(TNativeView nativeView)
		{

		}

		public virtual void UpdateValue(string property)
			=> mapper?.UpdateProperty(this, VirtualView, property);

		protected virtual void SetupDefaults() { }

		public ContainerView ContainerView { get; private set; }

		bool _hasContainer;
		public bool HasContainer
		{
			get => _hasContainer;
			set
			{
				if (_hasContainer == value)
					return;
				_hasContainer = value;
				if (value)
				{
					SetupContainer();
					_hasContainer = ContainerView != null;
				}
				else
					RemoveContainer();
			}

		}

		static protected Color CleanseColor(Color color, Color defaultColor) => color.IsDefault ? defaultColor : color;
	}
}