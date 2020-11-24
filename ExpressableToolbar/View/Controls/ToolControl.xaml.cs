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
    /// Interaction logic for ToolControl.xaml
    /// </summary>
    public partial class ToolControl : UserControl
    {
		public static readonly DependencyProperty ToolProperty = DependencyProperty.Register("Tool", typeof(ExpressTool), typeof(ToolControl), new FrameworkPropertyMetadata(default(ExpressTool)));
		public ExpressTool Tool
		{
			get
			{
				return GetValue(ToolProperty) as ExpressTool;
			}
			set
			{
				SetValue(ToolProperty, value);
			}
		}

		public ToolControl()
        {
            InitializeComponent();
        }

		private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			Tool?.OnToolInitialize(sender, e);
		}

		private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			Tool?.OnToolComplete(sender, e);
		}

		private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			Tool?.OnToolUpdate(sender, e);
		}
	}
}
