using ControlLibrary.Classes;
using Prism.Mvvm;
using System.IO;
using Newtonsoft.Json;
using ControlLibrary.Data;
using System.Windows;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;

namespace ModuleA.ViewModels
{
    class CommonViewModel : BindableBase
    {

        Autorun autorun = new Autorun();
        Settings settingsClass = new Settings();

        private ObservableCollection<Data> settings;

        private bool _switcher;
        public bool Switcher
        {
            get { return _switcher; }
            set 
            {
                SetProperty(ref _switcher, value);
                if (value == true)
                {
                    autorun.SetAutorunValue(true);
                    foreach (var setting in settings)
                    {
                        setting.Switcher = Switcher;
                    }
                    settingsClass.SaveSettings(settings);
                }
                else if (value == false)
                {
                    autorun.SetAutorunValue(false);
                    foreach (var setting in settings)
                    {
                        setting.Switcher = Switcher;
                    }
                    settingsClass.SaveSettings(settings);
                }
            }
        }

        public CommonViewModel()
        {
            Switch();
        }

        private void Switch()
        {
            settings = settingsClass.LoadSettings();
            foreach(var setting in settings)
            {
                Switcher = setting.Switcher;
            }
        }
    }
}
