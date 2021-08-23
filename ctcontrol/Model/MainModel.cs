using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ctcontrol.Model
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
            return "00T 00h 00m 00s";
        }
        public string SetBatary()
        {
            return (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + "%";
        }

        #endregion

    }
}
