using System.Windows;

namespace City.Classes
{
    static class Position
    {
        public static double GetLeft()
        {
            const int OffsetFromScreenBorder = 220;
            return SystemParameters.WorkArea.Right - OffsetFromScreenBorder;
        }

        public static double GetTop()
        {
            const int OffsetFromScreenBorder = 20;
            return SystemParameters.WorkArea.Top + OffsetFromScreenBorder;
        }
    }
}
