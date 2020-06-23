using System;
using Xamarin.Essentials;

namespace Xamarin_FRAIX_HUGO.Models
{
    public class MagnetometerTest
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;

        public float axeX, axeY, axeZ;
        public MagnetometerTest()
        {
            // Register for reading changes.
            Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;
        }

        void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            var data = e.Reading;
            // Process MagneticField X, Y, and Z
            axeX = data.MagneticField.X;
            axeY = data.MagneticField.Y;
            axeZ = data.MagneticField.Z;
        }

        public void ToggleMagnetometer()
        {
            try
            {
                if (!Magnetometer.IsMonitoring)
                {
                    Magnetometer.Start(speed);
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