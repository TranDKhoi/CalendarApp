using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Xamarin.Forms;
using Android.Content.Res;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using static CalendarApp.Droid.MainActivity;

[assembly: ResolutionGroupName("PlainEntryGroup")]
[assembly: ExportEffect(typeof(AndroidPlainEntryEffect), "PlainEntryEffect")]

namespace CalendarApp.Droid
{

    [Activity(Label = "CalendarApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public class AndroidPlainEntryEffect : PlatformEffect
        {
            public AndroidPlainEntryEffect()
            {
            }
            protected override void OnAttached()
            {
                try
                {
                    if (Control != null)
                    {
                        Android.Graphics.Color entryLineColor = Android.Graphics.Color.Transparent;
                        Control.BackgroundTintList = ColorStateList.ValueOf(entryLineColor);
                    }

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Error... Unable to set property on attached control", ex.Message);
                }
            }
            protected override void OnDetached()
            {
            }
            protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
            {
                base.OnElementPropertyChanged(args);
            }
        }
    }


}