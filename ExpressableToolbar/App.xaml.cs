using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ExpressableToolbar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ToolbarShadow shadow = new ToolbarShadow();
            MainWindow main = new MainWindow(shadow);
            SettingsWindow settings = new SettingsWindow();

            Current.MainWindow = main;
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            shadow.Show();
            main.Show();
            settings.Show();
        }
    }
}
