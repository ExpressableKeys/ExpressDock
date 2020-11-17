using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExpressableToolbar
{
    /// <summary>
    /// Interaction logic for ToolbarShadow.xaml
    /// </summary>
    public partial class ToolbarShadow : Window
    {
        public ToolbarShadow()
        {
 
            InitializeComponent();
        }
        public void FadeIn()
        {
            
        }
        public void Update(Window parentWindow)
        {
            var margin = 25;

            this.Left = parentWindow.Left - margin;
            this.Top = parentWindow.Top - margin;
            this.Width = parentWindow.Width + (2 * margin);
            this.Height = parentWindow.Height + (2 * margin);
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Make shadow unclickable
            var shadowHelper = new WindowInteropHelper(this);
            MainWindow.SetWindowLong(shadowHelper.Handle, MainWindow.GWL_EXSTYLE,
                MainWindow.GetWindowLong(shadowHelper.Handle, MainWindow.GWL_EXSTYLE) | MainWindow.WS_EX_TRANSPARENT | MainWindow.WS_EX_TOOLWINDOW);
        }
    }
}
