using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RunningAssistant
{
	[Activity (Label = "Running Assistant", MainLauncher = true)]
	public class HomeActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Home);

			Button button = FindViewById<Button> (Resource.Id.showPaceCalculatorButton);

			button.Click += (sender, e) => {
				StartActivity (typeof(CalculatorActivity));
			};

		}
	}
}

