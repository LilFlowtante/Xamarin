using System;
using Xamarin.Essentials;

namespace Xamarin_FRAIX_HUGO.Models
{
    class AccelerometerReader
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;

        public float axeX, axeY, axeZ;
        public AccelerometerReader()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            axeX = data.Acceleration.X;
            axeY = data.Acceleration.Y;
            axeZ = data.Acceleration.Z;
            // Process Acceleration X, Y, and Z
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (!Accelerometer.IsMonitoring) 
                { 
                    Accelerometer.Start(speed);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}