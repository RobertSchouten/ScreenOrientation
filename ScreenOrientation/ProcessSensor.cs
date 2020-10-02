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
    public class ProcessSensor
    {
        public Accelerometer _accelerometer;
        private AccelerometerReadingType type = AccelerometerReadingType.Standard;
        private bool currentlyUp;
        public ProcessSensor()
        {
            _accelerometer = Accelerometer.GetDefault(type);
            if (_accelerometer != null)
            {
                Console.WriteLine("accelerometer ready");

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
                throw new Exception("accelerometer not found");
            }
        }
        private void processReading(AccelerometerReading reading)
        {
            if (Helpers.getSlateState())
            { //if in laptop mode ensure screen is locked to keyboard even if upside down
                rotateScreen(true);
            }
            else
            {
                if (Helpers.getRotationLock())
                { //checks if user has rotation lock enabled which should bypass this rotation to permit portrait mode
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
