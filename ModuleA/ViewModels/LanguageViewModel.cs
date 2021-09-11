using Prism.Mvvm;

namespace ModuleA.ViewModels
{
    class LanguageViewModel : BindableBase
    {
        private string _title = "Language";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
