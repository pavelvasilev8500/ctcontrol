using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    class BackgroundViewModel : BindableBase
    {
        private string _text = "Background";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public BackgroundViewModel()
        {
        }
    }
}
