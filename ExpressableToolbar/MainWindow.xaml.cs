using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpressableToolbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<ExpressTool> Tools;
        private ToolbarShadow Shadow;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //Set the window style to noactivate.
            var mainHelper = new WindowInteropHelper(this);
            SetWindowLong(mainHelper.Handle, GWL_EXSTYLE,
                GetWindowLong(mainHelper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);

            HotKeyManager.SetupHotKeys(new WindowInteropHelper(this).Handle);
        }

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public MainWindow(ToolbarShadow shadow)
        {
            this.Shadow = shadow;
            InitializeComponent();

            Tools = new List<ExpressTool>();

            var tool = new ExpressBrushSettingTool();
            AddTool(tool);
        }

        public void AddTool(ExpressTool tool)
        {
            var column = new ColumnDefinition
            {
                Width = new GridLength(52)
            };

            ToolbarGrid.ColumnDefinitions.Add(column);
            ToolbarGrid.Children.Add(tool.ButtonControl);

            Grid.SetColumn(tool.ButtonControl, Tools.Count);

            if (Tools.Count > 0)
            {
                var margin = 10;
                var previousControl = Tools[Tools.Count - 1].ButtonControl;

                previousControl.Margin = new Thickness(5, 5, 0, 5);
                ToolbarGrid.ColumnDefinitions[Tools.Count - 1].Width = new GridLength(52 - 5);
            }

            tool.ButtonControl.Margin = new Thickness(5, 5, 5, 5);

            Tools.Add(tool);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Shadow.Update(this);
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            await Task.Delay(400);
            Shadow.Update(this);
        }
    }
}
