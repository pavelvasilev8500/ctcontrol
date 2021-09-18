using ModuleA.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ModuleA.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    class LanguageViewModel : BindableBase
    {

        CultureInfo russianCulture = new CultureInfo("ru-RU");
        CultureInfo englishCulture = new CultureInfo("en-US");

        public DelegateCommand ApplyCommand { get; private set; }

        public List<string> Languages { get; private set; } = new List<string>()
        {
            Properties.Resources.Russian,
            Properties.Resources.English
        };

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get 
            { 
                return _selectedLanguage; 
            }
            set 
            { 
                SetProperty(ref _selectedLanguage, value);
                if(value.Equals(Properties.Resources.Russian))
                {
                    Properties.Settings.Default.Culture = new CultureInfo("ru-RU", false);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                    Properties.Settings.Default.Save();
                }
                else if (value.Equals(Properties.Resources.English))
                {
                    Properties.Settings.Default.Culture = new CultureInfo("en-US", false);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    Properties.Settings.Default.Save();
                }
            }
        }

        public LanguageViewModel()
        {
            //Инициальзация при первом запуске
            FirsTimeInitialized();
            //Инициальзация при последующих запусках
            NextTimeInitialized();
            ApplyCommand = new DelegateCommand(Apply);
        }

        private void Apply()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void FirsTimeInitialized()
        {
            if (Properties.Settings.Default.Open == 0)
            {
                Properties.Settings.Default.Culture = CultureInfo.DefaultThreadCurrentCulture;
                if (CultureInfo.CurrentCulture.Equals(russianCulture))
                {
                    SelectedLanguage = Properties.Resources.Russian;
                }
                else
                {
                    SelectedLanguage = Properties.Resources.English;
                }
                Properties.Settings.Default.Open++;
                Properties.Settings.Default.Save();
            }
        }

        private void NextTimeInitialized()
        {
            CultureInfo.CurrentCulture = Properties.Settings.Default.Culture;
            if (CultureInfo.CurrentCulture.Equals(russianCulture))
            {
                SelectedLanguage = Properties.Resources.Russian;
            }
            else
            {
                SelectedLanguage = Properties.Resources.English;
            }
        }

    }
}
