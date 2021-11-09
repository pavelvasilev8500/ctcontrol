using Prism.Commands;
using Prism.Mvvm;
using ControlLibrary.Classes;
using System.Windows;
using System.Windows.Forms;
using System;
using Prism.Regions;
using ModuleA.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ModuleA.ViewModels
{
    public class MainViewModel : BindableBase
    {

        #region for test!
        private bool _title;
        public bool Title
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
        private readonly IRegionManager _regionManager;

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
        Timer timer = new Timer();
        Timer datatimer = new Timer();
        Server server = new Server();
        DataPC dataPC = new DataPC();
        #endregion

        #region Commands
        public DelegateCommand SleepCommand { get; private set; }
        public DelegateCommand ShutdonCommand { get; private set; }
        public DelegateCommand RebootCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand BlockCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand<string> SettingCommand { get; set; }
        #endregion

        #region Command Control
        private bool _canExecute = false;
        public bool CanExecute
        {
            get { return _canExecute; }
            set { SetProperty(ref _canExecute, value); }
        }
        #endregion

        public MainViewModel(IRegionManager regionManager)
        {
            #region Commands Realization
            SleepCommand = new DelegateCommand(Sleep, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            ShutdonCommand = new DelegateCommand(Shutdown, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            RebootCommand = new DelegateCommand(SystemReboot, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            ExitCommand = new DelegateCommand(Exit, CanExcuteMethod).ObservesCanExecute(() => CanExecute);
            BlockCommand = new DelegateCommand(Block);
            CloseCommand = new DelegateCommand(Close);
            SettingCommand = new DelegateCommand<string>(Settings);
            #endregion
            #region Methods
            StartDatabase();
            Startserver();
            StartClock();
            StartDataClock();
            mainModel.Initialize();
            #endregion
            #region Regions
            _regionManager = regionManager;
            #endregion
        }

        #region Methods
        private void StartClock()
        {
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            timer.Start();
        }

        private void StartDataClock()
        {
            datatimer.Enabled = true;
            datatimer.Tick += Datatimer_Tick;
            datatimer.Interval = 1000;
            datatimer.Start();
        }

        private void Controller()
        {

        }

        private void Datatimer_Tick(object sender, EventArgs e)
        {
            Data data = new Data { IDdata = "1", 
                date = $"{dataPC.SetDate()}", 
                time = $"{dataPC.SetTime()}", 
                day = $"{dataPC.SetDay()}", 
                worktime = $"{dataPC.SetWorkTime()}", 
                batary = $"{dataPC.SetBatary()}" };
            string json = JsonConvert.SerializeObject(data);
            try
            {
                FirsTimeInitializedAsync(json);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Date = mainModel.SetDate();
            Time = mainModel.SetTime();
            Second = mainModel.SetSecond();
            WorkTime = mainModel.SetWorkTime();
            Day = mainModel.SetDay();
            Batary = mainModel.SetBatary();
        }
        #endregion

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

        private void Settings(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void Close()
        {
            Environment.Exit(0);
        }
        #endregion

        #region StartServer&DataBase
        private void StartDatabase()
        {
            try
            {
                ProcessStartInfo database = new ProcessStartInfo();
                //Имя запускаемого приложения
                database.UseShellExecute = false;
                database.FileName = "cmd";
                //команда, которую надо выполнить
                database.Arguments = @"/k ""C://Program Files//MongoDB//Server//5.0//bin//mongod.exe"" --dbpath C:\apps\ctcontrol\data ";
                //  /c - после выполнения команды консоль закроется
                //  /к - не закрывать консоль после выполнения команды
                database.CreateNoWindow = true;
                Process.Start(database);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        private void Startserver()
        {
            try
            {
                ProcessStartInfo server = new ProcessStartInfo();
                //Имя запускаемого приложения
                server.UseShellExecute = false;
                server.FileName = "cmd";
                //команда, которую надо выполнить
                server.Arguments = @"/k node C://serverApps//NodeServer//app.js";
                //  /c - после выполнения команды консоль закроется
                //  /к - не закрывать консоль после выполнения команды
                server.CreateNoWindow = true;
                Process.Start(server);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async void FirsTimeInitializedAsync(string json)
        {
            await Task.Run(() => FirsTimeInitialized(json));
        }

        private string getIP()
        {
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    return "http://" + endPoint.Address.MapToIPv4().ToString() + ":4242/api/pcdata/";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void FirsTimeInitialized(string json)
        {
            string ip = getIP();
            try
            {
                WebRequest request = WebRequest.Create(ip);
                //WebRequest request = WebRequest.Create("http://192.168.0.107:4242/api/pcdata/");
                request.Timeout = 1000;
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = reader.ReadLine();
                        if (line.Equals("[]"))
                        {
                            server.Post(json, ip);
                        }
                        else
                        {
                            server.Put(json, ip);
                        }
                    }
                }
                response.Close();
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

    }
}
