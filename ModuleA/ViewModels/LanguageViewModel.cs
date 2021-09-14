using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    class LanguageViewModel : BindableBase
    {
        private string _text = "Language";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public LanguageViewModel()
        {
        }
    }
}
