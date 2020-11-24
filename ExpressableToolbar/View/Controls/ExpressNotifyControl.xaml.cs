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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpressableToolbar.View.Controls
{
	/// <summary>
	/// Interaction logic for ExpressNotifyControl.xaml
	/// </summary>
	public partial class ExpressNotifyControl : UserControl
	{
		public ExpressNotifyControl()
		{
			InitializeComponent();
		}

		private void ExpressDockIcon_PreviewTrayPopupOpen(object sender, RoutedEventArgs e)
		{
			new SettingsWindow().Show();
		}
	}
}
