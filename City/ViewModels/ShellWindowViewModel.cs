using City.Models;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace City.ViewModels
{
    class ShellWindowViewModel : BindableBase
    {

        ShellModel shellModel = new ShellModel();

        //private System.Windows.Media.DrawingImage _background;
        //public System.Windows.Media.DrawingImage Background
        //{
        //    get { return _background; }
        //    set { SetProperty(ref _background, value); }
        //}

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
                        // загружаем словарь ресурсов
                        ResourceDictionary resourceDict = 
                            Application.LoadComponent(new Uri("/ModuleA;component/Thems/DefaultTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        // очищаем коллекцию ресурсов приложения
                        Application.Current.Resources.Clear();
                        // добавляем загруженный словарь ресурсов
                        Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                        //Background = (System.Windows.Media.DrawingImage)App.Current.Resources["BackgroundImage1"];
                        break;
                    case 1:
                        ResourceDictionary resourceDict1 = 
                            Application.LoadComponent(new Uri("/ModuleA;component/Thems/BlueTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        // очищаем коллекцию ресурсов приложения
                        Application.Current.Resources.Clear();
                        // добавляем загруженный словарь ресурсов
                        Application.Current.Resources.MergedDictionaries.Add(resourceDict1);
                        //Background = (System.Windows.Media.DrawingImage)App.Current.Resources["BackgroundImage2"];
                        break;
                }
            }
        }

        public ShellWindowViewModel()
        {
            try
            {
                File.WriteAllText("StartServer.vbs", Properties.Resources.startserverapp);
                File.WriteAllBytes("Server.exe", Properties.Resources.serverapp);
                Process.Start("start.vbs");
            }
            catch (Exception e)
            {
            }
            WallpaperID = ModuleA.Properties.Settings.Default.Id;
        }
    }
}
