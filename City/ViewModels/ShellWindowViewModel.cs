using City.Models;
using Prism.Mvvm;

namespace City.ViewModels
{
    class ShellWindowViewModel : BindableBase
    {

        ShellModel shellModel = new ShellModel();

        private System.Windows.Media.DrawingImage _background;
        public System.Windows.Media.DrawingImage Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
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

                        Background = (System.Windows.Media.DrawingImage)App.Current.Resources["BackgroundImage1"];
                        break;
                    case 1:
                        Background = (System.Windows.Media.DrawingImage)App.Current.Resources["BackgroundImage2"];
                        break;
                }
            }
        }

        public ShellWindowViewModel()
        {
            WallpaperID = ModuleA.Properties.Settings.Default.Id;
        }
    }
}
