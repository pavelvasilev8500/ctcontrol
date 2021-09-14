using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
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
