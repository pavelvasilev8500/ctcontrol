using ctcontrol.Model;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace ctcontrol.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        readonly MainModel mm = new MainModel();
        readonly Reboot reboot = new Reboot();
        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public MainViewModel()
        {
            StartClock();
            _block = new Command(GoBlock, CanExecute);
            _shutdown = new Command(GoShutdown, CanExecute);
            _reboot = new Command(GoReboot, CanExecute);
            _exit = new Command(GoExit, CanExecute);
        }

        #region Data
        private string _data;
        private string _time;
        private string _second;
        private string _day;
        private string _worktime;
        private string _batary;
        private Command _block;
        private Command _sleep;
        private Command _shutdown;
        private Command _reboot;
        private Command _exit;


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

        #endregion

        #region Inteface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            MessageBox.Show("It's Work");
        }


        private void GoSleep(object parameter)
        {
            
        }

        private void GoShutdown(object parameter)
        {
            reboot.halt(false, false);
        }

        private void GoReboot(object parameter)
        {
            reboot.halt(true, false);
        }

        private void GoExit(object parameter)
        {
            reboot.Lock();
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion
    }
}
