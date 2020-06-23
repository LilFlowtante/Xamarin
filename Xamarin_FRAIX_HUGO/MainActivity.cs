using Android.App;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System;
using Xamarin_FRAIX_HUGO.Models;

namespace Xamarin_FRAIX_HUGO
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private AccelerometerReader accelerometerReader = new AccelerometerReader();
        public TextView VaxeX;
        public TextView VaxeY;
        public TextView VaxeZ;
        private MediaPlayer mp = new MediaPlayer();
        public string voiceChoose = "02";
        public Boolean voicePlay = false;
        public int nombrePenchement = 0;
        public Boolean parti = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main);

            VaxeX = FindViewById<TextView>(Resource.Id.textView1);
            VaxeY = FindViewById<TextView>(Resource.Id.textView2);
            VaxeZ = FindViewById<TextView>(Resource.Id.textView3);

            FindViewById<TextView>(Resource.Id.button1).Click += (o, e) =>
            {
                mp = MediaPlayer.Create(this, Resource.Raw.Voice01_01);
                voiceChoose = "01";
                Console.WriteLine(voiceChoose);
                mp.Start();

            };

            FindViewById<TextView>(Resource.Id.button2).Click += (o, e) =>
            {
                mp = MediaPlayer.Create(this, Resource.Raw.Voice02_01);
                voiceChoose = "02";
                Console.WriteLine(voiceChoose);
                mp.Start();

            };

            startTimer();

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void startTimer()
        {
            System.Timers.Timer Timer1 = new System.Timers.Timer();
            Timer1.Start();
            Timer1.Interval = 500;
            Timer1.Enabled = true;
            Timer1.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {
                RunOnUiThread(() =>
                {
                    Console.WriteLine("mp playing " + mp.IsPlaying.ToString());
                    if (!mp.IsPlaying)
                    {
                        voicePlay = false;
                    }

                    accelerometerReader.ToggleAccelerometer();
                    Console.WriteLine(accelerometerReader.axeZ);
                    if(accelerometerReader.axeX > 0 && parti == false)
                    {
                        if (voiceChoose.Equals("01"))
                        {
                            mp = MediaPlayer.Create(this, Resource.Raw.Voice01_02);
                        }
                        else if (voiceChoose.Equals("02")) {
                            mp = MediaPlayer.Create(this, Resource.Raw.Voice02_01);
                        }
                    }
                    if(accelerometerReader.axeZ < (float) 0.94 && voicePlay != true)
                    {
                        Console.WriteLine("Penché");
                        Console.WriteLine("voiceChoose " + voiceChoose.ToString());
                        if (voiceChoose.Equals("01"))
                        {
                            if (nombrePenchement <= 1)
                            {
                                mp = MediaPlayer.Create(this, Resource.Raw.Voice01_02);
                            } else {
                                mp = MediaPlayer.Create(this, Resource.Raw.Voice01_04);
                            }
                            voicePlay = true;
                            nombrePenchement++;
                            mp.Start();
                        } else if(voiceChoose.Equals("02")) {
                            if (nombrePenchement <= 1)
                            {
                                mp = MediaPlayer.Create(this, Resource.Raw.Voice02_02);
                            } else {
                                mp = MediaPlayer.Create(this, Resource.Raw.Voice02_04);
                            }
                            voicePlay = true;
                            nombrePenchement++;
                            mp.Start();
                        }
                    }

                    VaxeX.Text = accelerometerReader.axeX.ToString();
                    VaxeY.Text = accelerometerReader.axeY.ToString();
                    VaxeZ.Text = accelerometerReader.axeZ.ToString();
                    accelerometerReader.ToggleAccelerometer();
                });
            };
        }
    }
}