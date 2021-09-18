using ModuleA.ViewModels;
using System;
using System.Windows.Forms;

namespace ModuleA.Models
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
            int systemUptime = Environment.TickCount;
            var ts = TimeSpan.FromMilliseconds(systemUptime);
            return string.Format($"{ts.Days}D {ts.Hours}h {ts.Minutes}m {ts.Seconds}s");
        }
        public string SetBatary()
        {
            return (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + "%";
        }

        public void Initialize()
        {
            LanguageViewModel languageViewModel = new LanguageViewModel();
            BackgroundViewModel backgroundViewModel = new BackgroundViewModel();
            CommonViewModel commonViewModel = new CommonViewModel();
        }
        #endregion
    }
}
