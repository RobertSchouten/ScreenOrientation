using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenOrientation
{
    public static class Helpers
    {
        public static bool getSlateState()
        {
            //determine if device in tablet mode or laptop mode
            var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\PriorityControl", "ConvertibleSlateMode", true);
            return Convert.ToBoolean(value);
        }
        public static bool getRotationLock()
        {
            //get key to determine if rotation lock is on or not (also not enabled in laptop mode)

            var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AutoRotation", "Enable", false);
            return Convert.ToBoolean(value);
        }
    }
}
