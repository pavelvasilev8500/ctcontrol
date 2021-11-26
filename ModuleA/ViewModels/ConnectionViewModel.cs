using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ModuleA.ViewModels
{
    class ConnectionViewModel : BindableBase
    {
        public DelegateCommand<string> BackCommand { get; set; }

        private IRegionManager _regionManager;

        public ConnectionViewModel(IRegionManager regionManager)
        {
            BackCommand = new DelegateCommand<string>(Back);
            _regionManager = regionManager;
        }

        private void Back(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}
