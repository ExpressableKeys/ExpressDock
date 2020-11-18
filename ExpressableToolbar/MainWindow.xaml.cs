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

        private bool IsDragging;
        private Point StartPoint;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //Set the window style to noactivate.
            var mainHelper = new WindowInteropHelper(this);
            SetWindowLong(mainHelper.Handle, GWL_EXSTYLE,
                GetWindowLong(mainHelper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);

            HotKeyManager.SetupHotKeys(new WindowInteropHelper(this).Handle);

            Shadow.Update(this);
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
            IsDragging = false;

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
                var previousControl = Tools[Tools.Count - 1].ButtonControl;

                previousControl.Margin = new Thickness(5, 5, 0, 5);
                ToolbarGrid.ColumnDefinitions[Tools.Count - 1].Width = new GridLength(52 - 5);
            }

            tool.ButtonControl.Margin = new Thickness(5, 5, 5, 5);

            Tools.Add(tool);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            //Shadow.Update(this);
        }

        private void ToolbarBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                StartPoint = PointToScreen(Mouse.GetPosition(this));
                IsDragging = true;

                Mouse.Capture(ToolbarBorder);
            }
        }

        private void ToolbarBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDragging)
            {
                Mouse.Capture(null);
                IsDragging = false;
            }
        }

        private void ToolbarBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                Point newPoint = PointToScreen(Mouse.GetPosition(this));

                int diffX = (int)(newPoint.X - StartPoint.X);
                int diffY = (int)(newPoint.Y - StartPoint.Y);

                if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
                {
                    Left += diffX;
                    Top += diffY;

                    Shadow.Left += diffX;
                    Shadow.Top += diffY;

                    StartPoint = newPoint;
                }
            }
        }
    }
}
