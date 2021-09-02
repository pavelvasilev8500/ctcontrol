using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace City.Model
{
    public class MainModel
    {

    #region Methods

    public string SetDate()
        {
            DateTime now = DateTime.Now;
            return string.Format(now.ToString("dd. ") + "{0:Y}", now);
        }
        public string SetTime()
        {
            return DateTime.Now.ToString("HH:mm");
        }
        public string SetSecond()
        {
            return DateTime.Now.ToString("ss");
        }
        public string SetDay()
        {
            char[] day = DateTime.Now.ToString("dddd").ToCharArray();
            day[0] = char.ToUpper(day[0]);
            string Day = new string(day);
            return Day;
        }
        public string SetWorkTime()
        {
            // Get the system uptime
            int systemUptime = Environment.TickCount;
            //Using TimeSpan for Time in current format
            var ts = TimeSpan.FromMilliseconds(systemUptime);
            return string.Format($"{ts.Days}T {ts.Hours}h {ts.Minutes}m {ts.Seconds}s");
        }
        public string SetBatary()
        {
            return (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + "%";
        }

        #endregion

    }
}
