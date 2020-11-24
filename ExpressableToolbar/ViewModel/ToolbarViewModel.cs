using ExpressableToolbar.Properties;
using ExpressableToolbar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ExpressableToolbar.ViewModel
{
	class ToolbarViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<ExpressTool> _tools;
		public ObservableCollection<ExpressTool> Tools
		{
			get
			{
				return _tools;
			}
			set
			{
				_tools = value;
				OnPropertyChanged(nameof(Tools));
			}
		}

		public ToolbarViewModel()
		{
			_tools = new ObservableCollection<ExpressTool>();

			Tools.Add(new ExpressBrushRadiusTool());
			Tools.Add(new ExpressBrushHardnessTool());
			Tools.Add(new ExpressBrushOpacityTool());
			Tools.Add(new ExpressBrushFlowTool());
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
