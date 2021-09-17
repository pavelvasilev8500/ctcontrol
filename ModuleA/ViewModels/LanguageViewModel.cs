using ModuleA.Models;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace ModuleA.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    class LanguageViewModel : BindableBase
    {

        public List<string> Languages { get; private set; } = new List<string>()
        {
            (string)Application.Current.Resources["Russian"],
            (string)Application.Current.Resources["English"],
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
                if(value.Equals((string)Application.Current.Resources["Russian"]))
                {
                    Properties.Settings.Default.Culture = new CultureInfo("ru-RU", false);
                    Properties.Settings.Default.Save();
                    MessageBox.Show($"Russian");
                }
                else if (value.Equals((string)Application.Current.Resources["English"]))
                {
                    Properties.Settings.Default.Culture = new CultureInfo("en-US", false);
                    Properties.Settings.Default.Save();
                    MessageBox.Show($"English");
                }
            }
        }

        public LanguageViewModel()
        {
            var russianCulture = new CultureInfo("ru-RU");
            var englishCulture = new CultureInfo("en-US");
            if (Properties.Settings.Default.Open == 0)
            {
                Properties.Settings.Default.Culture = CultureInfo.CurrentCulture;
                if (CultureInfo.CurrentCulture.Equals(russianCulture))
                {
                    SelectedLanguage = (string)Application.Current.Resources["Russian"];
                }
                else
                {
                    SelectedLanguage = (string)Application.Current.Resources["English"];
                }
                Properties.Settings.Default.Open++;
                Properties.Settings.Default.Save();
            }
            CultureInfo.CurrentCulture = Properties.Settings.Default.Culture;
            if (CultureInfo.CurrentCulture.Equals(russianCulture))
            {
                SelectedLanguage = (string)Application.Current.Resources["Russian"];
            }
            else
            {
                SelectedLanguage = (string)Application.Current.Resources["English"];
            }
            MessageBox.Show($"CurrentCulture is now {CultureInfo.CurrentCulture.Name}.");
            MessageBox.Show($"Settings Culture {Properties.Settings.Default.Culture}.");
        }

    }
}
