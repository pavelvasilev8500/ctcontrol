using City.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLibrary.Classes;
using System.Windows.Forms;

namespace City.Models
{
    class ShellModel
    {
        CreatePCID createID = new CreatePCID();
        Timer timer = new Timer();
        private string FirstIp { get; set; } = GetPCIP.getIP();
        private string SecondIp { get; set; }

        public ShellModel()
        {
            StartClock();
        }

        public void SetAppID()
        {
            createID.CreateID();
        }

         private void StartClock()
        {
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SecondIp = GetPCIP.getIP();
            if (FirstIp != SecondIp)
            {
                FirstIp = SecondIp;
                try
                {
                    foreach (Process proc in Process.GetProcessesByName(ModuleA.Properties.Settings.Default.ProcessName))
                    {
                        proc.Kill();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                StartServer();
            }
        }

        public void StartServer()
        {
            try
            {
                string serverapp = Path.Combine(Path.GetTempPath(), "Server.exe");
                File.WriteAllBytes(serverapp, City.Properties.Resources.app);
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = serverapp,
                        CreateNoWindow = false,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                process.Start();
                ModuleA.Properties.Settings.Default.ProcessName = process.ProcessName;
                ModuleA.Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
            }
        }
    }


}
