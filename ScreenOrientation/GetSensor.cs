using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Windows.Devices.Sensors;
using System.Threading;

namespace ScreenOrientation
{
    public class GetSensor
    {
        private Accelerometer _accelerometer;
        private AccelerometerReadingType type = AccelerometerReadingType.Standard;
        private bool currentlyUp;
        private bool usePolling = true;
        public GetSensor()
        {
            _accelerometer = Accelerometer.GetDefault(type);
            if (_accelerometer != null)
            {
                Console.WriteLine("accelerometer ready");

                if (usePolling)
                {
                    //while should be running replace with proper
                    while (true)
                    {
                        //get accelerometer and proccess
                        processReading(_accelerometer.GetCurrentReading());
                        Thread.Sleep(50);
                    }
                }
                else
                {
                    //subscribe function to run on accelerometer change
                    _accelerometer.ReadingChanged += ReadingChanged;
                }
            }
            else
            {
                Console.WriteLine("accelerometer not found");
            }

            ////how frequently 
            _accelerometer.ReportInterval = Math.Max(_accelerometer.MinimumReportInterval, 1000000);

        }
        private void ReadingChanged(object sender, AccelerometerReadingChangedEventArgs e)
        {
            processReading(e.Reading);
        }
        private void processReading(AccelerometerReading reading)
        {
            if (Program.getSlateState())
            {
                rotateScreen(true);
            }
            else
            {
                if (Program.getRotationLock())
                {
                    if (reading.AccelerationY < -0.30)
                    {
                        rotateScreen(true);
                    }
                    if (reading.AccelerationY > 0.30)
                    {
                        rotateScreen(false);
                    }
                }
            }
        }
        private void rotateScreen(bool up)
        {
            if (up)
            {
                if (!currentlyUp)
                {
                    currentlyUp = true;
                    Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
                }
            } else
            {
                if (currentlyUp)
                {
                    currentlyUp = false;
                    Display.Rotate(1, Display.Orientations.DEGREES_CW_180);
                }
            }
        }
    }
}
