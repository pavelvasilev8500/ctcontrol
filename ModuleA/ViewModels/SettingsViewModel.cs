using ModuleA.Models;
using ModuleA.Properties;
using ModuleA.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;

namespace ModuleA.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
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

        #region Data
        private Visibility _langVisible = Visibility.Hidden;
        private Visibility _wallVisible = Visibility.Hidden;
        private Visibility _commVisible = Visibility.Hidden;

        public Visibility LangVisible
        {
            get
            {
                return _langVisible;
            }
            set
            {
                _langVisible = value;
                RaisePropertyChanged("LangVisible");
            }
        }

        public Visibility WallVisible
        {
            get
            {
                return _wallVisible;
            }
            set
            {
                _wallVisible = value;
                RaisePropertyChanged("WallVisible");
            }
        }

        public Visibility CommVisible
        {
            get
            {
                return _commVisible;
            }
            set
            {
                _commVisible = value;
                RaisePropertyChanged("CommVisible");
            }
        }
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
        #region Methods
        private void DefaultVisible()
        {
            LangVisible = Visibility.Hidden;
            WallVisible = Visibility.Hidden;
            CommVisible = Visibility.Hidden;
        }
        #endregion

        #region Command Methods
        private void Main(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void Language(string uri)
        {
            _regionManager.RequestNavigate("SettingsRegion", uri);
            DefaultVisible();
            LangVisible = Visibility.Visible;
            Title = Resources.Language;
        }

        private void Background(string uri)
        {
            _regionManager.RequestNavigate("SettingsRegion", uri);
            DefaultVisible();
            WallVisible = Visibility.Visible;
            Title = Resources.Background;
        }

        private void Common(string uri)
        {
            _regionManager.RequestNavigate("SettingsRegion", uri);
            DefaultVisible();
            CommVisible = Visibility.Visible;
            Title = Resources.Common;
        }

        private void SetDefaultSettings()
        {
            settingsModel.ResetSettings();
        }

        private void LoadDefaultSettingsView()
        {
            _regionManager.RegisterViewWithRegion<LanguageView>("SettingsRegion");
            DefaultVisible();
            LangVisible = Visibility.Visible;
            Title = Resources.Language;
        }
        #endregion
    }
}
//Title = (string)Application.Current.Resources["Language"];
