using System.Windows;
using ControlLibrary.Classes;
using ModuleA.ViewModels;

namespace City.Views
{
    /// <summary>
    /// Логика взаимодействия для ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
            this.Left = Position.GetLeft();
            this.Top = Position.GetTop();
            this.ShowInTaskbar = false;
        }

        private void Move(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
