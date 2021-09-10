using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB.ViewModels
{
    class SettingsViewModel : BindableBase
    {
        #region for test!
        private string _title = "Настройки";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> MainCommand { get; set; }
        public DelegateCommand<string> LanguageCommand { get; set; }

        public SettingsViewModel(IRegionManager regionManager)
        {
            MainCommand = new DelegateCommand<string>(Main);
            LanguageCommand = new DelegateCommand<string>(Language);
            _regionManager = regionManager;
        }

        private void Language(string uri)
        {
            _regionManager.RequestNavigate("SettingsContent", uri);
            Title = "Язык";
        }

        private void Main(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["SettingsContent"];

            var mainSettingView = containerProvider.Resolve<LanguageView>();
            region.Add(mainSettingView);
        }
    }
}
