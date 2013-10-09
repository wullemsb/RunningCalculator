using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Auth;
using Android.Support.V4.App;

namespace RunningAssistant
{
	[Activity (Label = "Running Assistant", MainLauncher = false)]
	public class CalculatorActivity : Activity
	{
		int count = 1;
		ListView splitItemsView;
		EditText hoursText;
		EditText minutesText;
		EditText secondsText;
		EditText distanceText;


		public static MobileServiceClient MobileService = new MobileServiceClient(
			"https://appname.azure-mobile.net/", 
			"appkey"
			);

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Calculator);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.calculateButton);
			splitItemsView = FindViewById<ListView>(Resource.Id.resultsListView); 
			distanceText = FindViewById<EditText> (Resource.Id.distanceText);
			hoursText = FindViewById<EditText> (Resource.Id.hoursText);
			minutesText = FindViewById<EditText> (Resource.Id.MinutesText);
			secondsText=FindViewById<EditText>(Resource.Id.SecondsText);

			LoadPreferences();

			button.Click += delegate {
				CalculatePace();
				SavePreferences();

			/*	button.Click += asyncdelegate {
				var table=MobileService.GetTable<Favorites>();
				await table.InsertAsync(new Favorites(){
					Hours = 2,
					Minutes = 2,
					Seconds = 1,
					User="test"
				});		

				var user= await MobileService.LoginAsync(this,MobileServiceAuthenticationProvider.MicrosoftAccount);

				AlertDialog alertMessage = new AlertDialog.Builder(this).Create();
				alertMessage.SetTitle("User:");
				alertMessage.SetMessage(user.ToString());
				alertMessage.Show();*/


			};
		}

		private void CalculatePace(){
			try {
				var paceCalculator=new PaceCalculator();
				var result=paceCalculator.Calc(new PaceCalculateParameters(){
					Distance =Convert.ToDouble(distanceText.Text),
					Hours = Convert.ToInt32(hoursText.Text),
					Minutes = Convert.ToInt32(minutesText.Text),
					Seconds = Convert.ToInt32(secondsText.Text)
				});

				// populate the listview with data
				splitItemsView.Adapter = new SplitItemAdapter(this, result.SplitItems);
				ShowNotification(result.ToString());
			} catch (Exception ex) {
				ShowNotification(ex.Message);
			}
		}

		private void LoadPreferences(){
		
			var prefs = this.GetSharedPreferences("RunningAssistant.preferences", FileCreationMode.Private);
			if (prefs.Contains("Distance")) {
				distanceText.Text = prefs.GetString ("Distance","");
			}
			if (prefs.Contains("Hours")) {
				hoursText.Text = prefs.GetString ("Hours","");
			}
			if (prefs.Contains("Minutes")) {
				minutesText.Text = prefs.GetString ("Minutes","");
			}
			if (prefs.Contains("Seconds")) {
				secondsText.Text = prefs.GetString ("Seconds","");
			}
		}


		private void SavePreferences(){
			var prefs = this.GetSharedPreferences("RunningAssistant.preferences", FileCreationMode.Private);
			var editor = prefs.Edit ();
			editor.PutString ("Distance", distanceText.Text);
			editor.PutString ("Hours", hoursText.Text);
			editor.PutString ("Minutes", minutesText.Text);
			editor.PutString ("Seconds", secondsText.Text);
			editor.Commit ();
		}

		private void ShowNotification(string text)
		{
			/*
			NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
				.SetContentTitle("Button Clicked")
				.SetSmallIcon(Resource.Drawable.Icon)
				.SetContentText(String.Format("The button has been clicked {0} times.",5));

			// Obtain a reference to the NotificationManager
			NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(1000, builder.Build());*/
			Android.Widget.Toast.MakeText(this, text, Android.Widget.ToastLength.Short).Show();
		}
	}
}


