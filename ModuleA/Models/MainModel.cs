using ControlLibrary.Classes;
using ModuleA.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        private bool Result { get; set; } = false;

        public MainModel()
        {
            CheckData();
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
                IDdata = $"{ControlLibrary.Properties.Settings.Default.PCID}",
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

        private void CheckData()
        {
            try
            {
                WebRequest request = WebRequest.Create(GetPCIP.getIP() + "/api/pcdata/");
                WebResponse response = request.GetResponse();
                request.Timeout = 100000;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        JArray o = JArray.Parse(reader.ReadToEnd());
                        foreach (var d in o)
                        {
                            if (ControlLibrary.Properties.Settings.Default.PCID.Equals(d["IDdata"].ToString()))
                            {
                                Result = true;
                                //MessageBox.Show($"{ControlLibrary.Properties.Settings.Default.PCID} - {d["IDdata"]} - {Result}");
                            }
                        }
                    }
                }
                response.Close();
            }
            catch (Exception)
            {
            }

        }

        private void FirsTimeInitialized(string json)
        {
            string ip = GetPCIP.getIP() + "/api/pcdata/";
            if (Result.Equals(true))
                server.Put(json, ip);
            else
            {
                server.Post(json, ip);
                Result = true;
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
