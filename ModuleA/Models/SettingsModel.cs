using ControlLibrary.Classes;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace ModuleA.Models
{
    public class SettingsModel
    {

        Autorun autorun = new Autorun(); 

        public void ResetSettings()
        {
            Properties.Settings.Default.Swither = false;
            autorun.SetAutorunValue(Properties.Settings.Default.Swither);
            Properties.Settings.Default.Culture = CultureInfo.DefaultThreadCurrentCulture;
            Properties.Settings.Default.Id = 0;
            Properties.Settings.Default.Save();
            //Перезагрузка приложения
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
