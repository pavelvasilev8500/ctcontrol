using ctcontrol.Model;
using System;
using System.Windows;

namespace ctcontrol.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        readonly MainModel mm = new MainModel();
        readonly Reboot reboot = new Reboot();
        readonly Autorun autorun = new Autorun();
        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public MainViewModel()
        {
            //Autorun enable
            //SetAutorunValue(true);
            //Autorun disable
            //SetAutorunValue(false);
            autorun.SetAutorunValue(true);
            StartClock();
            enableDisableCommand = new Command(() => {}, false);
            _block = new Command(GoBlock, true);
            _sleep = new Command(GoSleep);
            _shutdown = new Command(GoShutdown);
            _reboot = new Command(GoReboot);
            _exit = new Command(GoExit);
        }

        #region Data
        private string _data;
        private string _time;
        private string _second;
        private string _day;
        private string _worktime;
        private string _batary;
        private Visibility visibility;
        private Command _block;
        private Command _sleep;
        private Command _shutdown;
        private Command _reboot;
        private Command _exit;
        private Command enableDisableCommand;

        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
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
                _time = value;
                OnPropertyChanged("Time");
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
                _second = value;
                OnPropertyChanged("Second");
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
                _day = value;
                OnPropertyChanged("Day");
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
                _worktime = value;
                OnPropertyChanged("WorkTime");
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
                _batary = value;
                OnPropertyChanged("Batary");
            }
        }

        public Visibility VisibilityBlock
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged("VisibilityBlock");
            }
        }

        public Command Block
        {
            get
            {
                return _block;
            }
        }

        public Command Sleep
        {
            get 
            {
                return _sleep; 
            }
        }

        public Command Shutdown
        {
            get
            {
                return _shutdown;
            }
        }

        public Command Reboot
        {
            get
            {
                return _reboot;
            }
        }

        public Command Exit
        {
            get
            {
                return _exit;
            }
        }

        public Command EnableDisableCommand
        {
            get 
            { 
                return enableDisableCommand; 
            }
        }

        #endregion

        #region Methods

        private void StartClock()
        {
            _timer.Enabled = true;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Data = mm.SetDate();
            Time = mm.SetTime();
            Second = mm.SetSecond();
            Day = mm.SetDay();
            WorkTime = mm.SetWorkTime();
            Batary = mm.SetBatary();
        }

        private void GoBlock(object parameter)
        {
            if (EnableDisableCommand.CanExecute == false)
            {
                EnableDisableCommand.CanExecute = true;
                VisibilityBlock = Visibility.Hidden;
            }
            else if (EnableDisableCommand.CanExecute == true)
            {
                EnableDisableCommand.CanExecute = false;
                VisibilityBlock = Visibility.Visible;
            }
        }

        private void GoSleep(object parameter)
        {
            if (EnableDisableCommand.CanExecute == true)
            {
                reboot.Sleep(false, false, false);
            }
            else
                return;
        }

        private void GoShutdown(object parameter)
        {
            if (EnableDisableCommand.CanExecute == true)
                reboot.halt(false, false);
            else
                return;
        }

        private void GoReboot(object parameter)
        {
            if (EnableDisableCommand.CanExecute == true)
                reboot.halt(true, false);
            else
                return;
        }

        private void GoExit(object parameter)
        {
            if (EnableDisableCommand.CanExecute == true)
                reboot.Lock();
            else
                return;
        }

        #endregion
    }
}
