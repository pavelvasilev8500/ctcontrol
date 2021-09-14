using ModuleA.Models;
using ModuleA.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleA.ViewModels
{
    class SettingsViewModel : BindableBase
    {
        #region Title
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion

        #region Constractors
        IRegionManager _regionManager;
        SettingsModel settingsModel = new SettingsModel();
        #endregion

        #region Commands
        public DelegateCommand<string> MainCommand { get; set; }
        public DelegateCommand<string> LanguageCommand { get; set; }
        public DelegateCommand<string> BackgroundCommand { get; set; }
        public DelegateCommand<string> CommonCommand { get; set; }
        public DelegateCommand SetDefaultCommand { get; private set; }
        #endregion

        public SettingsViewModel(IRegionManager regionManager)
        {
            #region Commands Realization
            MainCommand = new DelegateCommand<string>(Main);
            LanguageCommand = new DelegateCommand<string>(Language);
            BackgroundCommand = new DelegateCommand<string>(Background);
            CommonCommand = new DelegateCommand<string>(Common);
            SetDefaultCommand = new DelegateCommand(SetDefaultSettings);
            #endregion
            _regionManager = regionManager;
            LoadDefaultSettingsView();
        }

        private void LoadDefaultSettingsView()
        {
            _regionManager.RegisterViewWithRegion<LanguageView>("SettingsRegion");
            Title = "Язык";
        }

        #region Command Methods
        private void Main(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void Language(string uri)
        {
            _regionManager.RequestNavigate("SettingsRegion", uri);
            Title = "Язык";
        }

        private void Background(string uri)
        {
            _regionManager.RequestNavigate("SettingsRegion", uri);
            Title = "Обои";
        }

        private void Common(string uri)
        {
            _regionManager.RequestNavigate("SettingsRegion", uri);
            Title = "Общие";
        }

        private void SetDefaultSettings()
        {
            settingsModel.ResetSettings();
        }

        #endregion
    }
}
