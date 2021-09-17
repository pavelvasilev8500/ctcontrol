using Prism.Mvvm;
using System.Windows;
using ControlLibrary.Classes;
using Prism.Regions;

namespace ModuleA.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    class CommonViewModel : BindableBase
    {
        Autorun autorun = new Autorun();

        private bool _switcher;
        public bool Switcher
        {
            get 
            {
                return _switcher; 
            }
            set 
            {
                SetProperty(ref _switcher, value);
                if (value == true)
                {
                    autorun.SetAutorunValue(value);
                    Properties.Settings.Default.Swither = Switcher;
                    Properties.Settings.Default.Save();
                }
                else if (value == false)
                {
                    autorun.SetAutorunValue(value);
                    Properties.Settings.Default.Swither = Switcher;
                    Properties.Settings.Default.Save();
                }
            }
        }

        public CommonViewModel()
        {
            this.Switcher = Properties.Settings.Default.Swither;
        }
    }
}
