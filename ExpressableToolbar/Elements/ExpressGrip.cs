using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ExpressableToolbar.Elements
{
    class ExpressGrip : ExpressElement
    {
        public ExpressGrip() : base()
        {
            ButtonControl.IsHitTestVisible = false;

            ButtonControl.Template = (ControlTemplate)Application.Current.MainWindow.Resources["HamburgerButton"];
        }
    }
}
