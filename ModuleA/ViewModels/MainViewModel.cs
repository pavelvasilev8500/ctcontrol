using Prism.Commands;
using Prism.Mvvm;
using ControlLibrary.Classes;
using ControlLibrary.Models;
using System.Windows;
using System.Windows.Forms;
using System;

namespace ModuleA.ViewModels
{
    public class MainViewModel : BindableBase
    {

        #region for test!
        private string _title = "Hello form ViewAViewModel";
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }
        #endregion

        #region Time&System&Control Info
        private string _date;
        private string _time;
        private string _second;
        private string _day;
        private string _worktime;
        private string _batary;
        private Visibility _blockvisible;

        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                SetProperty(ref _date, value);
            }
        }
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                SetProperty(ref _time, value);
            }
        }
        public string Second
        {
            get
            {
                return _second;
            }
            set
            {
                SetProperty(ref _second, value);
            }
        }
        public string Day
        {
            get
            {
                return _day;
            }
            set
            {
                SetProperty(ref _day, value);
            }
        }

        public string WorkTime
        {
            get
            {
                return _worktime;
            }
            set
            {
                SetProperty(ref _worktime, value);
            }
        }

        public string Batary
        {
            get
            {
                return _batary;
            }
            set
            {
                SetProperty(ref _batary, value);
            }
        }

        public Visibility Blockvisible
        {
            get
            {
                return _blockvisible;
            }
            set
            {
                _blockvisible = value;
                RaisePropertyChanged("Blockvisible");
            }
        }
        #endregion

        #region Constructors
        Reboot reboot = new Reboot();
        MainModel mainModel = new MainModel();
        Autorun autorun = new Autorun();
        Timer timer = new Timer();
        #endregion

        #region Commands
        public DelegateCommand SleepCommand { get; private set; }
        public DelegateCommand ShutdonCommand { get; private set; }
        public DelegateCommand RebootCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand BlockCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand SettingCommand { get; private set; }
        #endregion

        private bool _canExecute = false;
        public bool CanExecute
        {
            get { return _canExecute; }
            set { SetProperty(ref _canExecute, value); }
        }

        public MainViewModel()
        {
            #region Commands Realization
            SleepCommand = new DelegateCommand(Sleep, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            ShutdonCommand = new DelegateCommand(Shutdown, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            RebootCommand = new DelegateCommand(SystemReboot, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            ExitCommand = new DelegateCommand(Exit, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            BlockCommand = new DelegateCommand(Block);
            CloseCommand = new DelegateCommand(Close);
            SettingCommand = new DelegateCommand(Settings);
            #endregion
            StartClock();
            autorun.SetAutorunValue(true);
        }

        private void StartClock()
        {
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Date = mainModel.SetDate();
            Time = mainModel.SetTime();
            Second = mainModel.SetSecond();
            Day = mainModel.SetDay();
            WorkTime = mainModel.SetWorkTime();
            Batary = mainModel.SetBatary();
        }

        #region Command Methods
        private bool CanExcuteMethod()
        {
            return CanExecute;
        }

        private void Sleep()
        {
            reboot.Sleep(false, false, false);
        }

        private void Shutdown()
        {
            reboot.halt(false, false);
        }

        private void SystemReboot()
        {
            reboot.halt(true, false);
        }

        private void Exit()
        {
            reboot.Lock();
        }

        private void Block()
        {
            if (CanExecute == false)
            {
                Blockvisible = Visibility.Hidden;
                CanExecute = true;
            }
            else if (CanExecute == true)
            {
                Blockvisible = Visibility.Visible;
                CanExecute = false;
            }
        }

        private void Settings()
        {
            return;
        }

        private void Close()
        {
            Environment.Exit(0);
        }
        #endregion

    }
}
