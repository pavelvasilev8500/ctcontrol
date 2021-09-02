using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ctcontrol.Classes
{
    class Resolution
    {
        public double Width { get; set; }
        public double Height { get; set; }

        private double GetWidth()
        {
            var dpi = System.Windows.Media.VisualTreeHelper.GetDpi(new System.Windows.Controls.Control());
            Width = (SystemParameters.PrimaryScreenWidth * dpi.DpiScaleX);
            return Width;
        }

        private double GetHeight()
        {
            var dpi = System.Windows.Media.VisualTreeHelper.GetDpi(new System.Windows.Controls.Control());
            Height = (SystemParameters.PrimaryScreenHeight * dpi.DpiScaleY);
            return Height;
        }

        public Resolution()
        {
            Width = GetWidth();
            Height = GetHeight();
        }
    }
}
