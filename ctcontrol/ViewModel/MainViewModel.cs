using ctcontrol.Model;
using System;
using System.ComponentModel;

namespace ctcontrol.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        readonly MainModel mm = new MainModel();
        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        #region Data
        private string _data;
        private string _time;
        private string _second;
        private string _day;
        private string _worktime;
        private string _batary;

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

        #endregion

        #region Inteface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public MainViewModel()
        {
            StartClock();
        }

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

    }
}
