using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ModuleA.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    class BackgroundViewModel : BindableBase
    {

        public DelegateCommand NextCommand { get; set; }
        public DelegateCommand ApplyCommand { get; set; }
        public DelegateCommand PreviousCommand { get; set; }

        private System.Windows.Media.DrawingImage _background;
        public System.Windows.Media.DrawingImage Background
        {
            get { return _background; }
            set 
            {
                SetProperty(ref _background, value);
            }
        }

        private int _wallpaperID;
        public int WallpaperID
        {
            get
            {
                return _wallpaperID;
            }
            set
            {
                SetProperty(ref _wallpaperID, value);
                switch(WallpaperID)
                {
                    case 0:
                        Background = (System.Windows.Media.DrawingImage)Application.Current.Resources["BackgroundImage1"];
                        break;
                    case 1:
                        Background = (System.Windows.Media.DrawingImage)Application.Current.Resources["BackgroundImage2"];
                        break;
                    case 2:
                        Background = (System.Windows.Media.DrawingImage)Application.Current.Resources["BackgroundImage3"];
                        break;
                }
            }
        }

        public BackgroundViewModel()
        {
            NextCommand = new DelegateCommand(Next);
            ApplyCommand = new DelegateCommand(Apply);
            PreviousCommand = new DelegateCommand(Previous);
            WallpaperID = Properties.Settings.Default.Id;
        }

        private void Next()
        {
            if(Properties.Settings.Default.Id >= 2)
            {
                Properties.Settings.Default.Id = 0;
                Properties.Settings.Default.Save();
                WallpaperID = Properties.Settings.Default.Id;
            }
            else
            {
                Properties.Settings.Default.Id++;
                Properties.Settings.Default.Save();
                WallpaperID = Properties.Settings.Default.Id;
            }
        }

        private void Apply()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Previous()
        {
            if (Properties.Settings.Default.Id <= 0)
            {
                Properties.Settings.Default.Id = 2;
                Properties.Settings.Default.Save();
                WallpaperID = Properties.Settings.Default.Id;
                return;
            }
            else
            {
                Properties.Settings.Default.Id--;
                Properties.Settings.Default.Save();
                WallpaperID = Properties.Settings.Default.Id;
            }
        }
    }
}
