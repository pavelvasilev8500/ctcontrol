using ControlLibrary.Classes;
using ModuleA.Properties;
using ModuleA.ViewModels;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModuleA.Models
{
    public class MainModel
    {
        Timer datatimer = new Timer();
        Server server = new Server();
        DataPC dataPC = new DataPC();
        private string IP { get; set; }

        public MainModel()
        {
            StartDataClock();
        }

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

        private void StartDataClock()
        {
            datatimer.Enabled = true;
            datatimer.Tick += Datatimer_Tick;
            datatimer.Interval = 1000;
            datatimer.Start();
        }

        private void Datatimer_Tick(object sender, EventArgs e)
        {
            Data data = new Data
            {
                IDdata = "1",
                date = $"{dataPC.SetDate()}",
                time = $"{dataPC.SetTime()}",
                day = $"{dataPC.SetDay()}",
                worktime = $"{dataPC.SetWorkTime()}",
                batary = $"{dataPC.SetBatary()}"
            };
            string json = JsonConvert.SerializeObject(data);
            try
            {
                FirsTimeInitializedAsync(json);
            }
            catch (Exception)
            {
                return;
            }
        }

        private async void FirsTimeInitializedAsync(string json)
        {
            await Task.Run(() => FirsTimeInitialized(json));
        }

        private void FirsTimeInitialized(string json)
        {
            IP = GetPCIP.getIP() + "/api/pcdata/";
            string ip = IP;
            try
            {
                WebRequest request = WebRequest.Create(ip);
                request.Timeout = 1000;
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = reader.ReadLine();
                        if (line.Equals("[]"))
                        {
                            server.Post(json, ip);
                        }
                        else
                        {
                            server.Put(json, ip);
                        }
                    }
                }
                response.Close();
            }
            catch (Exception)
            {
                return;
            }
        }

        #region StartServer&DataBase
        private void StartDatabase()
        {
            try
            {
                ProcessStartInfo database = new ProcessStartInfo();
                //Имя запускаемого приложения
                database.UseShellExecute = false;
                database.FileName = "cmd";
                //команда, которую надо выполнить
                database.Arguments = @"/k ""C://Program Files//MongoDB//Server//5.0//bin//mongod.exe"" --dbpath C:\apps\ctcontrol\data ";
                //  /c - после выполнения команды консоль закроется
                //  /к - не закрывать консоль после выполнения команды
                database.CreateNoWindow = true;
                Process.Start(database);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        #endregion

        #endregion
    }
}
