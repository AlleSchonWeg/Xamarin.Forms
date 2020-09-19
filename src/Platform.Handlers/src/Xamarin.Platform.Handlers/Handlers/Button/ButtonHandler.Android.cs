﻿using AndroidX.AppCompat.Widget;

namespace Xamarin.Platform.Handlers
{
	public partial class ButtonHandler : AbstractViewHandler<IButton, AppCompatButton>
	{
		protected override AppCompatButton CreateView() => new AppCompatButton(Context);

		public static void MapText(IViewHandler handler, IButton view) 
		{
			(handler as ButtonHandler)
				.TypedNativeView.Text = view.Text;
		}
	}
}