using System;
using System.Windows;

namespace ctcontrol
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = GetLeft();
            Top = GetTop();
            ShowInTaskbar = false;
        }

        public void Move(object sender, EventArgs e)
        {
            DragMove();
        }

        private static double GetLeft()
        {
            const int OffsetFromScreenBorder = 200;
            return SystemParameters.WorkArea.Right - OffsetFromScreenBorder;
        }

        private static double GetTop()
        {
            const int OffsetFromScreenBorder = 20;
            return SystemParameters.WorkArea.Top + OffsetFromScreenBorder;
        }
    }
}
