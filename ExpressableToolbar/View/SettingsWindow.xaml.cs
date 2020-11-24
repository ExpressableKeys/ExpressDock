using ExpressableToolbar.ViewModel;
using System.Windows;

namespace ExpressableToolbar.View
{
	public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
			DataContext = new SettingsViewModel();
        }
    }
}
