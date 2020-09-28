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
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

        static void doStandby()
        {
            //Turn off the monitor
            SendMessage(0xFFFF, 0x112, 0xF170, 2);
        }

        public static bool getSlateState()
        {
            //determine if device in tablet mode or laptop mode
            var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\PriorityControl", "ConvertibleSlateMode", true);
            return Convert.ToBoolean(value);
        }
        public static bool getRotationLock()
        {
            //get key to determine if rotation lock is on or not (also not enabled in laptop mode)
            
            var value =Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AutoRotation", "Enable", false);
            return Convert.ToBoolean(value);
        }
        static void Main(string[] args)
        {

            GetSensor sensorfetch = new GetSensor();

            Console.ReadKey();
        }

        
    }
}
