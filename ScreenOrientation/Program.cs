using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Windows.Devices.Sensors;

namespace ScreenOrientation
{
    class Program
    {
        
        static void Main(string[] args)
        {

            ProcessSensor process = new ProcessSensor();

            Console.ReadKey();
        }

        
    }
}
