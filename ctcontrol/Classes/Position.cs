using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ctcontrol.Classes
{
    static class Position
    {
        public static double LeftPosition()
        {
            var primaryMonitorArea = SystemParameters.WorkArea;
            return primaryMonitorArea.Right - 220;
        }

        public static double TopPosition()
        {
            var primaryMonitorArea = SystemParameters.WorkArea;
            return primaryMonitorArea.Top + 20;
        }
    }
}
