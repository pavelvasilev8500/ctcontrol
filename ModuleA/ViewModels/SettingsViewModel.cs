using ModuleA.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ControlLibrary.Classes;

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
        private readonly IRegionManager _regionManager;
        Settings settingsClass = new Settings();
        #endregion

        #region Commands
        public DelegateCommand<string> MainCommand { get; set; }
        public DelegateCommand<string> LanguageCommand { get; set; }
        public DelegateCommand<string> CommonCommand { get; set; }
        public DelegateCommand SetDefaultCommand { get; private set; }
        #endregion

        public SettingsViewModel(IRegionManager regionManager)
        {
            #region Commands Realization
            MainCommand = new DelegateCommand<string>(Main);
            LanguageCommand = new DelegateCommand<string>(Language);
            CommonCommand = new DelegateCommand<string>(Common);
            SetDefaultCommand = new DelegateCommand(SetDefaultSettings);
            #endregion

            #region Region Setting
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion<LanguageView>("SettingsContent");
            Title = "Язык";
            #endregion   

            settingsClass.LoadSettings();
        }

        #region Command Methods
        private void Main(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void Language(string uri)
        {
            _regionManager.RequestNavigate("SettingsContent", uri);
            Title = "Язык";
        }

        private void Common(string uri)
        {
            _regionManager.RequestNavigate("SettingsContent", uri);
            Title = "Общие";
        }

        private void SetDefaultSettings()
        {
            settingsClass.SetDefaultData();
        }
        #endregion
    }
}
