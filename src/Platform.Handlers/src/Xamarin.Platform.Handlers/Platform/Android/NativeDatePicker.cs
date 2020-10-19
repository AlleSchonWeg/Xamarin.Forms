﻿using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace Xamarin.Platform
{
	public class NativeDatePicker : EditText, IOnClickListener
	{
		public NativeDatePicker(Context? context) : base(context)
		{
			Initialize();
		}

		public NativeDatePicker(Context? context, IAttributeSet attrs) : base(context, attrs)
		{
			Initialize();
		}

		public NativeDatePicker(Context? context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
			Initialize();
		}

		protected NativeDatePicker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		private void Initialize()
		{
			Focusable = true;
			SetOnClickListener(this);
		}

		public Action? ShowPicker { get; set; }
		public Action? HidePicker { get; set; }

		public void OnClick(View? v)
		{
			ShowPicker?.Invoke();
		}
	}
}