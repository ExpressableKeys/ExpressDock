using ExpressableToolbar.View;
using System.Windows;

namespace ExpressableToolbar
{
	public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ToolbarWindow main = new ToolbarWindow();

            Current.MainWindow = main;
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

			main.Show();
        }
    }
}
